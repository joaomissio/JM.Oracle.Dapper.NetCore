using Dapper.Oracle;
using Oracle.Dapper.NetCore.Storage;
using Oracle.Dapper.NetCore.Storage.Oracle;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace Oracle.Dapper.NetCore.Tests
{
    /// <summary>
    /// Checks that the official ODP.NET Core driver (Oracle.ManagedDataAccess.dll) loads and works
    /// on the current runtime. No database is needed.
    /// </summary>
    public class OracleDriverTests
    {
        private const string ConnectionString = "User Id=x;Password=y;Data Source=localhost:1521/FREEPDB1";

        private class LazyOracleConnection : OracleDbConnection
        {
            public LazyOracleConnection(string connectionString)
            {
                ConnectionString = connectionString;
            }

            public int OpenAttempts { get; private set; }

            protected override void OpenDbConnection(bool errorsExpected)
            {
                OpenAttempts++;
                base.OpenDbConnection(errorsExpected);
            }
        }

        [Fact]
        public void Driver_IsOfficialManagedDataAccess_23()
        {
            var assembly = typeof(OracleConnection).Assembly.GetName();

            Assert.Equal("Oracle.ManagedDataAccess", assembly.Name);
            Assert.Equal(23, assembly.Version.Major);
        }

        [Fact]
        public void OracleDbConnection_CreatesOracleConnection_WithoutOpening()
        {
            using var connection = new LazyOracleConnection(ConnectionString);

            var dbConnection = Assert.IsType<OracleConnection>(connection.DbConnection);
            Assert.Equal(ConnectionState.Closed, dbConnection.State);
            Assert.NotNull(connection.Database);
        }

        [Fact]
        public void OracleCommand_AcceptsDapperOracleParameters()
        {
            using var connection = new OracleConnection(ConnectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "PROCEDURE_NAME";
            command.CommandType = CommandType.StoredProcedure;

            var parameters = new OracleDynamicParameters();
            parameters.Add("PARAM1", 10, OracleMappingType.Int32);
            parameters.Add("PARAM2", "text", OracleMappingType.Varchar2);
            parameters.Add("OUTPUT_PARAM", null, OracleMappingType.RefCursor, ParameterDirection.Output);
            ((global::Dapper.SqlMapper.IDynamicParameters)parameters).AddParameters(command, null);

            Assert.Equal(3, command.Parameters.Count);
            Assert.Equal(OracleDbType.Int32, command.Parameters["PARAM1"].OracleDbType);
            Assert.Equal(OracleDbType.Varchar2, command.Parameters["PARAM2"].OracleDbType);
            Assert.Equal(OracleDbType.RefCursor, command.Parameters["OUTPUT_PARAM"].OracleDbType);
            Assert.Equal(ParameterDirection.Output, command.Parameters["OUTPUT_PARAM"].Direction);
        }

        [Theory]
        [InlineData(-1000)]
        [InlineData(3113)]
        [InlineData(3114)]
        [InlineData(12541)]
        [InlineData(12170)]
        public void RetryPolicy_TreatsConnectivityErrorsAsTransient(int oracleErrorNumber)
        {
            Assert.True(DatabaseCommunicationRetryPolicy.IsTransient(oracleErrorNumber));
        }

        [Theory]
        [InlineData(1)]      // unique constraint
        [InlineData(942)]    // table or view does not exist
        [InlineData(1017)]   // invalid username/password
        public void RetryPolicy_DoesNotRetryPermanentErrors(int oracleErrorNumber)
        {
            Assert.False(DatabaseCommunicationRetryPolicy.IsTransient(oracleErrorNumber));
        }

        [Fact]
        public void RetryPolicy_DoesNotRetryNonOracleExceptions()
        {
            var calls = 0;
            var policy = new DatabaseCommunicationRetryPolicy();

            Assert.Throws<InvalidOperationException>(() => policy.Execute(() =>
            {
                calls++;
                throw new InvalidOperationException();
            }));
            Assert.Equal(1, calls);
        }

        [Fact]
        public void OpenOracleConnection_UnreachableServer_ThrowsOracleException()
        {
            // port 1 is never an Oracle listener: proves the driver's network stack runs on this runtime
            using var connection = new LazyOracleConnection("User Id=x;Password=y;Data Source=127.0.0.1:1/NOPE;Connection Timeout=5");

            var exception = Assert.Throws<OracleException>(() => connection.Open());

            // driver 23.x reports ORA-50201 and keeps ORA-12541 (no listener) in the inner exception
            Assert.True(DatabaseCommunicationRetryPolicy.IsTransient(exception), exception.ToString());
        }

        [Fact]
        public void OpenOracleConnection_UnreachableServer_RetriesThreeTimes()
        {
            using var connection = new LazyOracleConnection("User Id=x;Password=y;Data Source=127.0.0.1:1/NOPE;Connection Timeout=5");

            var exception = Assert.Throws<OracleException>(() => connection.OpenOracleConnection(true));
            Assert.True(connection.OpenAttempts == 4, $"attempts={connection.OpenAttempts} {exception}");
        }

        [Fact]
        public void OpenOracleConnection_InvalidConnectString_DoesNotRetry()
        {
            using var connection = new LazyOracleConnection("User Id=x;Password=y;Data Source=(DESCRIPTION=broken");

            Assert.ThrowsAny<Exception>(() => connection.OpenOracleConnection(true));
            Assert.Equal(1, connection.OpenAttempts);
        }
    }
}

using Oracle.Dapper.NetCore.Dapper;
using Oracle.ManagedDataAccess.Client;
using Polly.Retry;
using System.Data.Common;

namespace Oracle.Dapper.NetCore.Storage.Oracle
{
    public class OracleDbConnection : RelationalConnection, IOracleDbConnection
    {
        private IDapperFeatures _database;
        private readonly IRetryPolicy _retryPolicy;

        public OracleDbConnection()
        {
            _retryPolicy = new DatabaseCommunicationRetryPolicy();
        }

        public virtual IDapperFeatures Database { get => _database ?? GetDatabaseInstance(); }

        public void OpenOracleConnection(bool errorsExpected)
        {
            _retryPolicy.Execute(() => Open(errorsExpected));
        }

        protected override DbConnection CreateDbConnection()
            => new OracleConnection(GetValidatedConnectionString());

        private IDapperFeatures GetDatabaseInstance()
        {
            _database = new DapperFeatures(DbConnection);
            return _database;
        }
    }
}

using Oracle.Dapper.NetCore.Dapper;
using Oracle.ManagedDataAccess.Client;
using System.Data.Common;

namespace Oracle.Dapper.NetCore.Storage.Oracle
{
    public class OracleDbConnection : RelationalConnection, IOracleDbConnection
    {
        private IDapperFeatures _database;
        public virtual IDapperFeatures Database { get => _database ?? GetDatabaseInstance(); }

        protected override void OpenDbConnection(bool errorsExpected)
        {
            if (errorsExpected
                && DbConnection is OracleConnection oracleConnection)
            {
                oracleConnection.Open();
            }
            else
            {
                DbConnection.Open();
            }
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

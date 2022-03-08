using Oracle.Dapper.NetCore.Infrastructure;
using Oracle.Dapper.NetCore.Storage.Oracle;

namespace Oracle.Dapper.NetCore.Context
{
    public class OracleContext : OracleDbConnection, IDbContext
    {
        public OracleContext(string connectionString)
        {
            ConnectionString = connectionString;
            CreateDbConnection();
            OpenOracleConnection(true);
        }
    }
}

using Oracle.Dapper.NetCore.Infrastructure;
using Oracle.Dapper.NetCore.Storage.Oracle;

namespace Oracle.Dapper.NetCore.Context
{
    /// <summary>
    /// Oracle Context for use in repositories
    /// To use multiple contexts in the application, implement the OracleContext class inheritance and register all contexts in the dependency injection
    /// </summary>
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

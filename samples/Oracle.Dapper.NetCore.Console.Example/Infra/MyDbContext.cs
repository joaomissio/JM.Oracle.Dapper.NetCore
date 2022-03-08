using Oracle.Dapper.NetCore.Context;

namespace Oracle.Dapper.NetCore.Console.Example.Infra
{
    public class MyDbContext : OracleContext
    {
        public MyDbContext(string connectionString)
            : base(connectionString)
        {
        }
    }
}

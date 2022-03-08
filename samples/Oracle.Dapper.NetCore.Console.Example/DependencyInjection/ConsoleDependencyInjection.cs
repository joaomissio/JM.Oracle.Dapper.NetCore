using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Oracle.Dapper.NetCore.Console.Example.Infra;
using Oracle.Dapper.NetCore.DependencyInjection;

namespace Oracle.Dapper.NetCore.Console.Example.DependencyInjection
{
    public static class ConsoleDI
    {
        public static void RegisterConsoleDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IExampleRepository, ExampleRepository>();
            //Register Oracle Dependency Scoped
            services.AddScopedOracleDapper<MyDbContext>(configuration.GetConnectionString("myOracleConnectionString"));
            //Register Oracle Dependency Transient
            //services.AddTransientOracleDapper<MyDbContext>(configuration.GetConnectionString("myOracleConnectionString"));
        }
    }
}

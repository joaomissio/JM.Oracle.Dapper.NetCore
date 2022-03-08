using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Oracle.Dapper.NetCore.Console.Example.Core;
using Oracle.Dapper.NetCore.Console.Example.DependencyInjection;
using Oracle.Dapper.NetCore.Console.Example.HostServices;
using System.Threading.Tasks;

namespace Oracle.Dapper.NetCore.Console.Example
{
    public class Startup : StartupConsoleBase
    {
        private readonly IConfiguration _configuration;
        public Startup()
        {
            _configuration = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json")
              .AddEnvironmentVariables()
              .Build();
        }

        public override async Task ConfigureConsole(string[] args)
        {
            var hostBuilder = CreateHostBuilder(args);
            hostBuilder.ConfigureHostConfiguration(ConfigureHostConfiguration);
            hostBuilder.ConfigureAppConfiguration(ConfigureAppConfiguration);
            hostBuilder.ConfigureServices(ConfigureServices);
            hostBuilder.ConfigureLogging(ConfigureLogging);
            await hostBuilder.RunConsoleAsync();
        }

        public virtual new void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            base.ConfigureServices(context, services);
            services.RegisterConsoleDependencies(_configuration);
            services.AddHostedService<OracleExampleHostService>();
        }
    }
}

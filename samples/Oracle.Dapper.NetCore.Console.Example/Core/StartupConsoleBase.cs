using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace Oracle.Dapper.NetCore.Console.Example.Core
{
    public abstract class StartupConsoleBase
    {
        /// <summary>
        /// Criar uma instancia do HostBuilder
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args);
        }

        /// <summary>
        /// Possibilitar a configuração do console customizada
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public abstract Task ConfigureConsole(string[] args);

        /// <summary>
        /// Configuração dos serviços da aplicação, injeção de dependencias...
        /// </summary>
        /// <param name="context"></param>
        /// <param name="services"></param>
        protected virtual void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
        }

        /// <summary>
        /// Configuração de log da aplicação
        /// </summary>
        /// <param name="context"></param>
        /// <param name="loggingBuilder"></param>
        protected virtual void ConfigureLogging(HostBuilderContext context, ILoggingBuilder loggingBuilder)
        {
        }

        /// <summary>
        /// Configurações da aplicação
        /// </summary>
        /// <param name="context"></param>
        /// <param name="configBuilder"></param>
        protected virtual void ConfigureAppConfiguration(HostBuilderContext context, IConfigurationBuilder configBuilder)
        {
            configBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configBuilder.AddJsonFile("appsettings.json");
            configBuilder.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", true);
            configBuilder.AddEnvironmentVariables();
        }

        /// <summary>
        /// Configurações de Host
        /// </summary>
        /// <param name="configBuilder"></param>
        protected virtual void ConfigureHostConfiguration(IConfigurationBuilder configBuilder)
        { }

        /// <summary>
        /// Configurações padrões para inicilizar um app console
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual async Task ConfigureDefaultConsole(string[] args)
        {
            var hostBuilder = CreateHostBuilder(args);
            hostBuilder.ConfigureHostConfiguration(ConfigureHostConfiguration);
            hostBuilder.ConfigureAppConfiguration(ConfigureAppConfiguration);
            hostBuilder.ConfigureServices(ConfigureServices);
            hostBuilder.ConfigureLogging(ConfigureLogging);
            await hostBuilder.RunConsoleAsync();
        }
    }
}

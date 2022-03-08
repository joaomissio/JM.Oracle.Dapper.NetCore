using Microsoft.Extensions.Hosting;
using Oracle.Dapper.NetCore.Console.Example.Infra;
using System.Threading;
using System.Threading.Tasks;

namespace Oracle.Dapper.NetCore.Console.Example.HostServices
{
    public class OracleExampleHostService : BackgroundService
    {
        private readonly IExampleRepository _exampleRepository;

        public OracleExampleHostService(IExampleRepository exampleRepository)
        {
            _exampleRepository = exampleRepository;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var resultado = _exampleRepository.GetDays();

            if (resultado != null)
            {
                foreach (var item in resultado)
                {
                    System.Console.WriteLine(item);
                }
            }
            return Task.CompletedTask;
        }
    }
}

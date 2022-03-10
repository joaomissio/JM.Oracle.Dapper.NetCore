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

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var resultado = _exampleRepository.GetDays();
            var resultado2 = await _exampleRepository.GetDaysAsync();
            if (resultado != null && resultado2 != null)
            {
                System.Console.WriteLine("Result of GetDays:");
                foreach (var item in resultado)
                {
                    System.Console.WriteLine(item);
                }

                System.Console.WriteLine("Result of GetDaysAsync:");
                foreach (var item in resultado2)
                {
                    System.Console.WriteLine(item);
                }
            }
        }
    }
}

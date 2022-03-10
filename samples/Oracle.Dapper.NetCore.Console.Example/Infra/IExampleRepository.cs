using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oracle.Dapper.NetCore.Console.Example.Infra
{
    public interface IExampleRepository
    {
        IEnumerable<string> GetDays();
        Task<IEnumerable<string>> GetDaysAsync();
    }
}

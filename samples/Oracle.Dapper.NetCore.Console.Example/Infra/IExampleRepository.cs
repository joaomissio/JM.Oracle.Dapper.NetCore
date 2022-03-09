using System.Collections.Generic;

namespace Oracle.Dapper.NetCore.Console.Example.Infra
{
    public interface IExampleRepository
    {
        IEnumerable<string> GetDays();
    }
}

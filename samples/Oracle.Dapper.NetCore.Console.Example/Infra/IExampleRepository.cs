using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oracle.Dapper.NetCore.Console.Example.Infra
{
    public interface IExampleRepository
    {
        IEnumerable<string> GetDays();
    }
}

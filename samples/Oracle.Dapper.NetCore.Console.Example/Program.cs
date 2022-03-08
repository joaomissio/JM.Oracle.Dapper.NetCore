using System;
using System.Threading.Tasks;

namespace Oracle.Dapper.NetCore.Console.Example
{
    internal static class Program
    {
        private async static Task Main(string[] args)
        {
            await new Startup()
                .ConfigureConsole(args);
        }
    }
}

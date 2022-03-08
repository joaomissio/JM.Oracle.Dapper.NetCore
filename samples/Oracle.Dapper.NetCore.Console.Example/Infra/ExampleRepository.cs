using System.Collections.Generic;

namespace Oracle.Dapper.NetCore.Console.Example.Infra
{
    public class ExampleRepository : IExampleRepository
    {
        private readonly MyDbContext _context;
        public ExampleRepository(MyDbContext context)
        {
            _context = context;
        }

        public IEnumerable<string> GetDays()
        {
            return _context.Database.Query<string>(QueryDays);
        }

        private const string QueryDays = @"
        WITH TESTE (DIA) 
        AS
        (
            SELECT 'SEGUNDA' FROM DUAL
            UNION
            SELECT 'TERÇA' FROM DUAL
            UNION
            SELECT 'QUARTA' FROM DUAL
            UNION
            SELECT 'QUINTA' FROM DUAL
            UNION
            SELECT 'SEXTA' FROM DUAL
            UNION
            SELECT 'SABADO' FROM DUAL
            UNION
            SELECT 'DOMINGO' FROM DUAL
        )
        SELECT DIA FROM TESTE
        ";
    }
}

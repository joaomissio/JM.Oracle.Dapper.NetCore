using Dapper;
using Dapper.Oracle;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<string>> GetDaysAsync()
        {
            var parameters = new OracleDynamicParameters();
            parameters.Add("PRIMEIRO_DIA_SEMANA", "DOMINGO", OracleMappingType.Varchar2);
            parameters.Add("SEGUNDO_DIA_SEMANA", "SEGUNDA", OracleMappingType.Varchar2);
            parameters.Add("TERCEIRO_DIA_SEMANA", "TERÇA", OracleMappingType.Varchar2);
            parameters.Add("QUARTO_DIA_SEMANA", "QUARTA", OracleMappingType.Varchar2);
            parameters.Add("QUINTO_DIA_SEMANA", "QUINTA", OracleMappingType.Varchar2);
            parameters.Add("SEXTO_DIA_SEMANA", "SEXTA", OracleMappingType.Varchar2);
            parameters.Add("SETIMO_DIA_SEMANA", "SABADO", OracleMappingType.Varchar2);
            return await _context.Database.QueryAsync<string>(QueryDaysWithParameters, parameters);
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

        private const string QueryDaysWithParameters = @"
        WITH TESTE (DIA) 
        AS
        (
            SELECT :PRIMEIRO_DIA_SEMANA FROM DUAL
            UNION
            SELECT :SEGUNDO_DIA_SEMANA FROM DUAL
            UNION
            SELECT :TERCEIRO_DIA_SEMANA FROM DUAL
            UNION
            SELECT :QUARTO_DIA_SEMANA FROM DUAL
            UNION
            SELECT :QUINTO_DIA_SEMANA FROM DUAL
            UNION
            SELECT :SEXTO_DIA_SEMANA FROM DUAL
            UNION
            SELECT :SETIMO_DIA_SEMANA FROM DUAL
        )
        SELECT DIA FROM TESTE
        ";
    }
}

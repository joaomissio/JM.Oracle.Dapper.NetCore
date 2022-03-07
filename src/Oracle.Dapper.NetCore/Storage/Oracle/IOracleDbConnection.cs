using Oracle.Dapper.NetCore.Dapper;

namespace Oracle.Dapper.NetCore.Storage.Oracle
{
    public interface IOracleDbConnection : IRelationalConnection
    {
        IDapperFeatures Database { get; }
    }
}

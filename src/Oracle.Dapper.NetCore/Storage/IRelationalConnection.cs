using Oracle.Dapper.NetCore.Infrastructure;
using System;
using System.Data.Common;

namespace Oracle.Dapper.NetCore.Storage
{
    public interface IRelationalConnection : IRelationalTransactionManager, IDisposable
    {
        string? ConnectionString { get;}
        DbConnection DbConnection { get;}
        DbContext Context { get; }
        new IDbContextTransaction? CurrentTransaction { get; }
        Guid ConnectionId { get; }
        bool Open(bool errorsExpected = false);
        bool Close();
    }
}

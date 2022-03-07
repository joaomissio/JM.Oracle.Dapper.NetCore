using System;
using System.Data.Common;

namespace Oracle.Dapper.NetCore.Storage
{
    public interface IDbContextTransaction : IDisposable, IInfrastructure<DbTransaction>
    {
        Guid TransactionId { get; }
        void Commit();
        void Rollback();
    }
}

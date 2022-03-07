using System;
using System.Data;

namespace Oracle.Dapper.NetCore.Storage
{
    public interface IRelationalTransactionManager : IDbContextTransactionManager
    {
        IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel);
    }

    public interface IDbContextTransactionManager
    {
        IDbContextTransaction BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        IDbContextTransaction? CurrentTransaction { get; }
    }
}

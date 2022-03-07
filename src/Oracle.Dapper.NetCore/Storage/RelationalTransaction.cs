using System;
using System.Data.Common;

namespace Oracle.Dapper.NetCore.Storage
{
    public class RelationalTransaction : IDbContextTransaction
    {
        private readonly DbTransaction _dbTransaction;
        private bool _connectionClosed;
        private bool _disposed;
        private readonly bool _transactionOwned;
        protected virtual IRelationalConnection Connection { get; }
        public virtual Guid TransactionId { get; }

        public RelationalTransaction(
                IRelationalConnection connection,
                DbTransaction transaction,
                Guid transactionId,
                bool transactionOwned)
        {
            if (connection.DbConnection != transaction.Connection)
            {
                throw new InvalidOperationException("A transação especificada não está associada à conexão atual. Apenas transações associadas à conexão atual podem ser usadas");
            }
            Connection = connection;
            TransactionId = transactionId;
            _dbTransaction = transaction;
            _transactionOwned = transactionOwned;
        }

        public void Commit()
        {
            _dbTransaction.Commit();
            ClearTransaction();
        }

        public void Rollback()
        {
            _dbTransaction.Rollback();
            ClearTransaction();
        }

        protected virtual void ClearTransaction()
        {
            if (!_connectionClosed)
            {
                _connectionClosed = true;

                Connection.Close();
            }
        }

        DbTransaction IInfrastructure<DbTransaction>.Instance
        => _dbTransaction;

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;

                if (_transactionOwned)
                {
                    _dbTransaction.Dispose();
                }

                ClearTransaction();
            }
        }
    }
}

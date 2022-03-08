using Oracle.Dapper.NetCore.Infrastructure;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Data.Common;

namespace Oracle.Dapper.NetCore.Storage
{
    public abstract class RelationalConnection : IRelationalConnection
    {
        private string? _connectionString;
        private DbConnection? _connection;
        private int _openedCount;

        public virtual string? ConnectionString
        {
            get => _connectionString ?? _connection?.ConnectionString;
            internal set
            {
                if (_connection != null && _connection.ConnectionString != value)
                {
                    _connection.ConnectionString = value;
                }

                _connectionString = value;
            }
        }

        protected virtual string GetValidatedConnectionString()
        {
            var connectionString = ConnectionString;
            if (connectionString == null)
            {
                throw new InvalidOperationException("Um armazenamento relacional foi configurado sem especificar o DbConnection ou a string de conexão a ser usada");
            }

            return connectionString;
        }

        public virtual DbConnection DbConnection
        {
            get
            {
                if (_connection == null
                    && _connectionString == null)
                {
                    throw new InvalidOperationException("Um armazenamento relacional foi configurado sem especificar o DbConnection ou a string de conexão a ser usada");
                }

                return _connection ??= CreateDbConnection();
            }
            internal set
            {
                if (!ReferenceEquals(_connection, value))
                {
                    if (_openedCount > 0)
                    {
                        throw new InvalidOperationException("A instância de DbConnection está atualmente em uso. A conexão só pode ser alterada quando a conexão existente não está sendo usada");
                    }

                    Dispose();

                    _connection = value;
                    _connectionString = null;
                }
            }
        }

        public virtual DbContext Context { get; }

        public virtual IDbContextTransaction CurrentTransaction { get; protected set; }

        public virtual Guid ConnectionId { get; } = Guid.NewGuid();

        private IDbContextTransaction CreateRelationalTransaction(DbTransaction transaction, Guid transactionId, bool transactionOwned)
        => CurrentTransaction
            = CreateRelationalTransaction(
                this,
                transaction,
                transactionId,
                transactionOwned: transactionOwned);

        private RelationalTransaction CreateRelationalTransaction(IRelationalConnection connection, DbTransaction transaction, Guid transactionId, bool transactionOwned)
            => new RelationalTransaction(connection, transaction, transactionId, transactionOwned);

        public virtual void CommitTransaction()
        {
            if (CurrentTransaction == null)
            {
                throw new InvalidOperationException("A conexão não tem nenhuma transação ativa");
            }

            CurrentTransaction.Commit();
        }

        public virtual void RollbackTransaction()
        {
            if (CurrentTransaction == null)
            {
                throw new InvalidOperationException("A conexão não tem nenhuma transação ativa");
            }

            CurrentTransaction.Rollback();
        }

        public virtual bool Open(bool errorsExpected = false)
        {
            if (DbConnection.State == ConnectionState.Broken)
            {
                CloseDbConnection();
            }

            var wasOpened = false;
            if (DbConnection.State != ConnectionState.Open)
            {
                CurrentTransaction?.Dispose();
                OpenDbConnection(errorsExpected);
                wasOpened = true;
            }
            _openedCount++;
            return wasOpened;
        }

        public virtual bool Close()
        {
            var wasClosed = false;

            if (ShouldClose())
            {
                CurrentTransaction?.Dispose();
                CurrentTransaction = null;
                if (DbConnection.State != ConnectionState.Closed)
                {
                    try
                    {
                        CloseDbConnection();
                        _openedCount = 0;
                        wasClosed = true;
                    }
                    catch
                    {
                        Console.WriteLine("Erro ao fechar conexão");
                        throw;
                    }
                }
            }

            return wasClosed;
        }

        protected abstract DbConnection CreateDbConnection();

        protected virtual void OpenDbConnection(bool errorsExpected)
        {
            if (errorsExpected
                 && DbConnection is OracleConnection oracleConnection)
            {
                oracleConnection.Open();
            }
            else
            {
                DbConnection.Open();
            }
        }

        protected virtual void CloseDbConnection()
            => DbConnection.Close();

        protected virtual void ResetState(bool disposeDbConnection)
        {
            CurrentTransaction?.Dispose();
            CurrentTransaction = null;
            _openedCount = 0;

            if (disposeDbConnection
                && _connection != null)
            {
                DisposeDbConnection();
                _connection = null;
                _openedCount = 0;
            }
        }

        protected virtual void DisposeDbConnection()
        => DbConnection.Dispose();

        private bool ShouldClose()
        => _openedCount == 0 || (_openedCount > 0 && --_openedCount == 0);

        public virtual void Dispose()
        => ResetState(disposeDbConnection: true);

        public virtual IDbContextTransaction BeginTransaction()
        => BeginTransaction(IsolationLevel.Unspecified);

        public virtual IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            Open();

            EnsureNoTransactions();

            var transactionId = Guid.NewGuid();

            var dbTransaction = ConnectionBeginTransaction(isolationLevel);

            return CreateRelationalTransaction(dbTransaction, transactionId, true);
        }

        protected virtual DbTransaction ConnectionBeginTransaction(IsolationLevel isolationLevel)
        => DbConnection.BeginTransaction(isolationLevel);

        private void EnsureNoTransactions()
        {
            if (CurrentTransaction != null)
            {
                throw new InvalidOperationException("A conexão já está em uma transação e não pode participar de outra transação");
            }
        }
    }
}

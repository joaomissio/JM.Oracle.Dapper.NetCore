using Oracle.Dapper.NetCore.Tests.Fakes;
using System.Data;

namespace Oracle.Dapper.NetCore.Tests
{
    public class TransactionTests
    {
        [Fact]
        public void Commit_ClearsCurrentTransaction_AndKeepsConnectionOpen()
        {
            using var connection = new FakeRelationalConnection();

            connection.BeginTransaction();
            connection.CommitTransaction();

            Assert.Null(connection.CurrentTransaction);
            Assert.Equal(1, connection.Fake.Transactions[0].CommitCalls);
            Assert.True(connection.Fake.Transactions[0].WasDisposed);
            Assert.Equal(ConnectionState.Open, connection.Fake.State);
        }

        [Fact]
        public void Rollback_ClearsCurrentTransaction_AndKeepsConnectionOpen()
        {
            using var connection = new FakeRelationalConnection();

            connection.BeginTransaction();
            connection.RollbackTransaction();

            Assert.Null(connection.CurrentTransaction);
            Assert.Equal(1, connection.Fake.Transactions[0].RollbackCalls);
            Assert.Equal(ConnectionState.Open, connection.Fake.State);
        }

        [Fact]
        public void SequentialTransactions_OnSameConnection_Work()
        {
            using var connection = new FakeRelationalConnection();

            connection.BeginTransaction();
            connection.CommitTransaction();
            connection.BeginTransaction();
            connection.RollbackTransaction();
            connection.BeginTransaction(IsolationLevel.Serializable);
            connection.CommitTransaction();

            Assert.Equal(3, connection.Fake.Transactions.Count);
            Assert.Equal(IsolationLevel.Serializable, connection.Fake.Transactions[2].IsolationLevel);
            Assert.Equal(ConnectionState.Open, connection.Fake.State);
        }

        [Fact]
        public void SequentialTransactions_CommittedThroughCurrentTransaction_Work()
        {
            using var connection = new FakeRelationalConnection();

            connection.BeginTransaction().Commit();
            connection.BeginTransaction().Rollback();
            connection.BeginTransaction();
            connection.CommitTransaction();

            Assert.Equal(3, connection.Fake.Transactions.Count);
        }

        [Fact]
        public void BeginTransaction_WhileAnotherIsActive_Throws()
        {
            using var connection = new FakeRelationalConnection();

            connection.BeginTransaction();

            Assert.Throws<InvalidOperationException>(() => connection.BeginTransaction());
        }

        [Fact]
        public void CommitOrRollback_WithoutTransaction_Throws()
        {
            using var connection = new FakeRelationalConnection();

            Assert.Throws<InvalidOperationException>(() => connection.CommitTransaction());
            Assert.Throws<InvalidOperationException>(() => connection.RollbackTransaction());
        }

        [Fact]
        public void FailedCommit_KeepsTransactionActive_SoItCanBeRolledBack()
        {
            using var connection = new FakeRelationalConnection();

            connection.BeginTransaction();
            connection.Fake.Transactions[0].ThrowOnCommit = true;

            Assert.Throws<InvalidOperationException>(() => connection.CommitTransaction());
            Assert.NotNull(connection.CurrentTransaction);

            connection.RollbackTransaction();
            Assert.Equal(1, connection.Fake.Transactions[0].RollbackCalls);
            Assert.Null(connection.CurrentTransaction);

            connection.BeginTransaction();
            connection.CommitTransaction();
        }

        [Fact]
        public void TransactionCycles_DoNotLeakOpenCount()
        {
            var connection = new FakeRelationalConnection();

            for (var i = 0; i < 5; i++)
            {
                connection.BeginTransaction();
                connection.CommitTransaction();
            }

            Assert.True(connection.Close());
            Assert.Equal(ConnectionState.Closed, connection.Fake.State);
        }

        [Fact]
        public void Dispose_DisposesConnectionAndActiveTransaction()
        {
            var connection = new FakeRelationalConnection();
            connection.BeginTransaction();

            connection.Dispose();

            Assert.True(connection.Fake.WasDisposed);
            Assert.True(connection.Fake.Transactions[0].WasDisposed);
            Assert.Null(connection.CurrentTransaction);
        }

        [Fact]
        public void Open_AfterClose_ReopensConnection()
        {
            using var connection = new FakeRelationalConnection();

            Assert.True(connection.Close());
            Assert.True(connection.Open());
            Assert.Equal(ConnectionState.Open, connection.Fake.State);
        }
    }
}

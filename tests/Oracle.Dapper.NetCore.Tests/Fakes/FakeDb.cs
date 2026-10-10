using Oracle.Dapper.NetCore.Storage;
using System.Collections;
using System.Data;
using System.Data.Common;

namespace Oracle.Dapper.NetCore.Tests.Fakes
{
    internal class FakeDbConnection : DbConnection
    {
        private ConnectionState _state = ConnectionState.Closed;

        public int OpenCalls { get; private set; }
        public int CloseCalls { get; private set; }
        public bool WasDisposed { get; private set; }
        public List<FakeDbTransaction> Transactions { get; } = new();
        public DataTable Result { get; set; } = new();

        public override string ConnectionString { get; set; }
        public override string Database => "fake";
        public override string DataSource => "fake";
        public override string ServerVersion => "0";
        public override ConnectionState State => _state;

        public override void ChangeDatabase(string databaseName) { }

        public override void Open()
        {
            OpenCalls++;
            _state = ConnectionState.Open;
        }

        public override void Close()
        {
            CloseCalls++;
            _state = ConnectionState.Closed;
        }

        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            var transaction = new FakeDbTransaction(this, isolationLevel);
            Transactions.Add(transaction);
            return transaction;
        }

        protected override DbCommand CreateDbCommand() => new FakeDbCommand(this);

        protected override void Dispose(bool disposing)
        {
            WasDisposed = true;
            _state = ConnectionState.Closed;
            base.Dispose(disposing);
        }
    }

    internal class FakeDbTransaction : DbTransaction
    {
        private readonly FakeDbConnection _connection;

        public FakeDbTransaction(FakeDbConnection connection, IsolationLevel isolationLevel)
        {
            _connection = connection;
            IsolationLevel = isolationLevel;
        }

        public int CommitCalls { get; private set; }
        public int RollbackCalls { get; private set; }
        public bool WasDisposed { get; private set; }
        public bool ThrowOnCommit { get; set; }

        public override IsolationLevel IsolationLevel { get; }
        protected override DbConnection DbConnection => _connection;

        public override void Commit()
        {
            CommitCalls++;
            if (ThrowOnCommit)
            {
                throw new InvalidOperationException("commit failed");
            }
        }

        public override void Rollback() => RollbackCalls++;

        protected override void Dispose(bool disposing)
        {
            WasDisposed = true;
            base.Dispose(disposing);
        }
    }

    internal class FakeDbCommand : DbCommand
    {
        private readonly FakeDbConnection _connection;

        public FakeDbCommand(FakeDbConnection connection)
        {
            _connection = connection;
        }

        public override string CommandText { get; set; }
        public override int CommandTimeout { get; set; }
        public override CommandType CommandType { get; set; }
        public override bool DesignTimeVisible { get; set; }
        public override UpdateRowSource UpdatedRowSource { get; set; }
        protected override DbConnection DbConnection { get; set; }
        protected override DbParameterCollection DbParameterCollection { get; } = new FakeDbParameterCollection();
        protected override DbTransaction DbTransaction { get; set; }

        public override void Cancel() { }
        public override int ExecuteNonQuery() => 0;
        public override object ExecuteScalar() => null;
        public override void Prepare() { }
        protected override DbParameter CreateDbParameter() => throw new NotSupportedException();
        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior) => _connection.Result.CreateDataReader();
    }

    internal class FakeDbParameterCollection : DbParameterCollection
    {
        private readonly List<object> _items = new();

        public override int Count => _items.Count;
        public override object SyncRoot => _items;

        public override int Add(object value)
        {
            _items.Add(value);
            return _items.Count - 1;
        }

        public override void AddRange(Array values) => _items.AddRange(values.Cast<object>());
        public override void Clear() => _items.Clear();
        public override bool Contains(object value) => _items.Contains(value);
        public override bool Contains(string value) => IndexOf(value) >= 0;
        public override void CopyTo(Array array, int index) => ((ICollection)_items).CopyTo(array, index);
        public override IEnumerator GetEnumerator() => _items.GetEnumerator();
        public override int IndexOf(object value) => _items.IndexOf(value);
        public override int IndexOf(string parameterName) => _items.FindIndex(p => ((DbParameter)p).ParameterName == parameterName);
        public override void Insert(int index, object value) => _items.Insert(index, value);
        public override void Remove(object value) => _items.Remove(value);
        public override void RemoveAt(int index) => _items.RemoveAt(index);
        public override void RemoveAt(string parameterName) => _items.RemoveAt(IndexOf(parameterName));
        protected override DbParameter GetParameter(int index) => (DbParameter)_items[index];
        protected override DbParameter GetParameter(string parameterName) => (DbParameter)_items[IndexOf(parameterName)];
        protected override void SetParameter(int index, DbParameter value) => _items[index] = value;
        protected override void SetParameter(string parameterName, DbParameter value) => _items[IndexOf(parameterName)] = value;
    }

    /// <summary>
    /// Mirrors OracleContext: the connection is opened when the context is created.
    /// </summary>
    internal class FakeRelationalConnection : RelationalConnection
    {
        public FakeDbConnection Fake { get; } = new();

        public FakeRelationalConnection()
        {
            ConnectionString = "fake";
            Open();
        }

        protected override DbConnection CreateDbConnection() => Fake;
    }
}

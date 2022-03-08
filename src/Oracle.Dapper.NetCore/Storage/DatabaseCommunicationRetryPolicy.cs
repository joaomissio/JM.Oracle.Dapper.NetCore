using Oracle.ManagedDataAccess.Client;
using Polly;
using System;
using System.Linq;

namespace Oracle.Dapper.NetCore.Storage
{
    public interface IRetryPolicy
    {
        void Execute(Action operation);
    }

    public class DatabaseCommunicationRetryPolicy : IRetryPolicy
    {
        private const int RetryCount = 3;
        private const int WaitBetweenRetriesInMilliseconds = 1000;
        private readonly int[] _oracleSqlExceptions = new[] { -1000 };
        private readonly Policy _retryPolicy;
        public DatabaseCommunicationRetryPolicy()
        {
            _retryPolicy = Policy
            .Handle<OracleException>(exception => _oracleSqlExceptions.Contains(exception.Number))
            .WaitAndRetry(
                retryCount: RetryCount,
                sleepDurationProvider: _ => TimeSpan.FromMilliseconds(WaitBetweenRetriesInMilliseconds)
            );
        }

        public void Execute(Action operation)
        {
            _retryPolicy.Execute(operation.Invoke);
        }
    }
}

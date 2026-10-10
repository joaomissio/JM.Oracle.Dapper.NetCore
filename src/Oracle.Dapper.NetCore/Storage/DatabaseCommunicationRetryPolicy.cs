using Oracle.ManagedDataAccess.Client;
using Polly;
using System;
using System.Linq;
using System.Text.RegularExpressions;

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
        // Transient connectivity errors: pool timeout (-1000 / ORA-50000), listener/network failures and dropped sessions
        private static readonly int[] OracleTransientErrors = new[]
        {
            -1000, 50000,
            3113, 3114, 3135,
            12170, 12514, 12528, 12537, 12541, 12545, 12571
        };
        private static readonly Regex OracleErrorCode = new Regex(@"ORA-(\d{5})", RegexOptions.Compiled);
        private readonly Policy _retryPolicy;
        public DatabaseCommunicationRetryPolicy()
        {
            _retryPolicy = Policy
            .Handle<OracleException>(IsTransient)
            .WaitAndRetry(
                retryCount: RetryCount,
                sleepDurationProvider: _ => TimeSpan.FromMilliseconds(WaitBetweenRetriesInMilliseconds)
            );
        }

        /// <summary>
        /// The driver reports network failures as a generic error (ORA-50201) and keeps the real cause
        /// (e.g. ORA-12541) in the inner exceptions, so the whole chain is inspected.
        /// </summary>
        internal static bool IsTransient(Exception exception)
        {
            for (var current = exception; current != null; current = current.InnerException)
            {
                if (current is OracleException oracleException && IsTransient(oracleException.Number))
                {
                    return true;
                }

                foreach (Match match in OracleErrorCode.Matches(current.Message ?? string.Empty))
                {
                    if (IsTransient(int.Parse(match.Groups[1].Value)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        internal static bool IsTransient(int oracleErrorNumber)
            => OracleTransientErrors.Contains(oracleErrorNumber);

        public void Execute(Action operation)
        {
            _retryPolicy.Execute(operation.Invoke);
        }
    }
}

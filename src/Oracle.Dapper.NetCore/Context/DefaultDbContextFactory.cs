using Oracle.Dapper.NetCore.CustomExceptions;
using Oracle.Dapper.NetCore.Infrastructure;
using System;
using System.Reflection;
using System.Runtime.ExceptionServices;

namespace Oracle.Dapper.NetCore.Context
{
    internal class DefaultDbContextFactory<TContext> where TContext : IDbContext
    {
        public TContext CreateDbContext(string connectionString)
        {
            var provider = GetProviderContext(connectionString);
            try
            {
                return (TContext)Activator.CreateInstance(typeof(TContext), provider.ConnectionString);
            }
            catch (TargetInvocationException exception) when (exception.InnerException != null)
            {
                // surface the real error (e.g. OracleException) instead of the reflection wrapper
                ExceptionDispatchInfo.Capture(exception.InnerException).Throw();
                throw;
            }
        }

        private static ProviderContext GetProviderContext(string connectionString)
        {
            return CreateProviderContext(connectionString);
        }

        private static ProviderContext CreateProviderContext(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new NotFoundException("Oracle connection string not found");
            }
            return new ProviderContext("Oracle", connectionString);
        }
    }
}
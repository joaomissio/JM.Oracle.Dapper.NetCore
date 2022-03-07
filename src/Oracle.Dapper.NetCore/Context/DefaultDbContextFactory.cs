using Oracle.Dapper.NetCore.CustomExceptions;
using Oracle.Dapper.NetCore.Infrastructure;
using System;

namespace Oracle.Dapper.NetCore.Context
{
    public class DefaultDbContextFactory<TContext> where TContext : IDbContext
    {
        public TContext CreateDbContext(string connectionString)
        {
            var provider = GetProviderContext(connectionString);
            return (TContext)Activator.CreateInstance(typeof(TContext), provider.ConnectionString);
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
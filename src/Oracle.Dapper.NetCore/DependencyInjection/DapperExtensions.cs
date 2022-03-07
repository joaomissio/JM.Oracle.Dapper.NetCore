using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Oracle.Dapper.NetCore.Context;
using Oracle.Dapper.NetCore.Infrastructure;
using Oracle.Dapper.NetCore.Storage;
using Oracle.Dapper.NetCore.Storage.Oracle;
using System;

namespace Oracle.Dapper.NetCore.DependencyInjection
{
    public static class DapperExtensions
    {
        public static void AddScopedOracleDapper<TContext>(this IServiceCollection services, string connectionString)
            where TContext : class, IDbContext
        {
            services.TryAddScoped<IOracleDbConnection, OracleDbConnection>();
            services.TryAddScoped<IRelationalConnection>(p => p.GetRequiredService<IOracleDbConnection>());
            services.TryAddScoped(provider => ResolveDbContextWithDapper<TContext>(provider, connectionString));
        }

        public static void AddTransientOracleDapper<TContext>(this IServiceCollection services, string connectionString)
            where TContext : class, IDbContext
        {
            services.TryAddTransient<IOracleDbConnection, OracleDbConnection>();
            services.TryAddTransient<IRelationalConnection>(p => p.GetRequiredService<IOracleDbConnection>());
            services.TryAddTransient(provider => ResolveDbContextWithDapper<TContext>(provider, connectionString));
        }

        private static TContext ResolveDbContextWithDapper<TContext>(IServiceProvider provider, string connectionString)
            where TContext : IDbContext
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            return new DefaultDbContextFactory<TContext>().CreateDbContext(connectionString);
        }
    }
}

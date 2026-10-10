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
        /// <summary>
        /// register oracle dependencies as scoped
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        public static IServiceCollection AddScopedOracleDapper<TContext>(this IServiceCollection services, string connectionString)
            where TContext : class, IDbContext
            => AddOracleDapper<TContext>(services, _ => connectionString, ServiceLifetime.Scoped);

        /// <summary>
        /// register oracle dependencies as scoped, resolving the connection string when the context is created
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="services"></param>
        /// <param name="connectionStringFactory"></param>
        public static IServiceCollection AddScopedOracleDapper<TContext>(this IServiceCollection services, Func<IServiceProvider, string> connectionStringFactory)
            where TContext : class, IDbContext
            => AddOracleDapper<TContext>(services, connectionStringFactory, ServiceLifetime.Scoped);

        /// <summary>
        /// register oracle dependencies as transient - suitable for parallel and multi-thread executions
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        public static IServiceCollection AddTransientOracleDapper<TContext>(this IServiceCollection services, string connectionString)
            where TContext : class, IDbContext
            => AddOracleDapper<TContext>(services, _ => connectionString, ServiceLifetime.Transient);

        /// <summary>
        /// register oracle dependencies as transient, resolving the connection string when the context is created
        /// - suitable for parallel and multi-thread executions
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="services"></param>
        /// <param name="connectionStringFactory"></param>
        public static IServiceCollection AddTransientOracleDapper<TContext>(this IServiceCollection services, Func<IServiceProvider, string> connectionStringFactory)
            where TContext : class, IDbContext
            => AddOracleDapper<TContext>(services, connectionStringFactory, ServiceLifetime.Transient);

        private static IServiceCollection AddOracleDapper<TContext>(IServiceCollection services, Func<IServiceProvider, string> connectionStringFactory, ServiceLifetime lifetime)
            where TContext : class, IDbContext
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (connectionStringFactory is null)
            {
                throw new ArgumentNullException(nameof(connectionStringFactory));
            }

            services.TryAdd(new ServiceDescriptor(
                typeof(TContext),
                provider => ResolveDbContextWithDapper<TContext>(provider, connectionStringFactory(provider)),
                lifetime));

            // IOracleDbConnection resolves to the first registered context, so it is always a usable connection
            if (typeof(IOracleDbConnection).IsAssignableFrom(typeof(TContext)))
            {
                services.TryAdd(new ServiceDescriptor(
                    typeof(IOracleDbConnection),
                    provider => provider.GetRequiredService<TContext>(),
                    lifetime));
            }
            else
            {
                services.TryAdd(new ServiceDescriptor(typeof(IOracleDbConnection), typeof(OracleDbConnection), lifetime));
            }

            services.TryAdd(new ServiceDescriptor(
                typeof(IRelationalConnection),
                provider => provider.GetRequiredService<IOracleDbConnection>(),
                lifetime));

            return services;
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

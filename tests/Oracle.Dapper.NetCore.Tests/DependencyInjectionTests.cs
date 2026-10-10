using Microsoft.Extensions.DependencyInjection;
using Oracle.Dapper.NetCore.CustomExceptions;
using Oracle.Dapper.NetCore.DependencyInjection;
using Oracle.Dapper.NetCore.Infrastructure;
using Oracle.Dapper.NetCore.Storage;
using Oracle.Dapper.NetCore.Storage.Oracle;

namespace Oracle.Dapper.NetCore.Tests
{
    public class DependencyInjectionTests
    {
        private const string ConnectionString = "User Id=x;Password=y;Data Source=localhost:1521/FREEPDB1";

        // Same shape as OracleContext, but does not open the connection on creation
        private class LazyOracleContext : OracleDbConnection, IDbContext
        {
            public LazyOracleContext(string connectionString)
            {
                ConnectionString = connectionString;
            }
        }

        private class OtherOracleContext : LazyOracleContext
        {
            public OtherOracleContext(string connectionString) : base(connectionString) { }
        }

        private class FailingContext : IDbContext
        {
            public FailingContext(string connectionString)
            {
                throw new InvalidOperationException("cannot connect to " + connectionString);
            }
        }

        [Fact]
        public void AddScoped_RegistersScopedServices_AndReturnsCollection()
        {
            var services = new ServiceCollection();

            var returned = services.AddScopedOracleDapper<LazyOracleContext>(ConnectionString);

            Assert.Same(services, returned);
            Assert.All(services, descriptor => Assert.Equal(ServiceLifetime.Scoped, descriptor.Lifetime));
            Assert.Contains(services, descriptor => descriptor.ServiceType == typeof(LazyOracleContext));
            Assert.Contains(services, descriptor => descriptor.ServiceType == typeof(IOracleDbConnection));
            Assert.Contains(services, descriptor => descriptor.ServiceType == typeof(IRelationalConnection));
        }

        [Fact]
        public void AddTransient_RegistersTransientServices()
        {
            var services = new ServiceCollection();

            services.AddTransientOracleDapper<LazyOracleContext>(ConnectionString);

            Assert.All(services, descriptor => Assert.Equal(ServiceLifetime.Transient, descriptor.Lifetime));
        }

        [Fact]
        public void Scoped_ResolvesSameContextPerScope_WithConnectionString()
        {
            using var provider = new ServiceCollection()
                .AddScopedOracleDapper<LazyOracleContext>(ConnectionString)
                .BuildServiceProvider(validateScopes: true);

            using var scope = provider.CreateScope();
            using var otherScope = provider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<LazyOracleContext>();

            Assert.Equal(ConnectionString, context.ConnectionString);
            Assert.Same(context, scope.ServiceProvider.GetRequiredService<LazyOracleContext>());
            Assert.Same(context, scope.ServiceProvider.GetRequiredService<IOracleDbConnection>());
            Assert.Same(context, scope.ServiceProvider.GetRequiredService<IRelationalConnection>());
            Assert.NotSame(context, otherScope.ServiceProvider.GetRequiredService<LazyOracleContext>());
        }

        [Fact]
        public void Transient_ResolvesNewContextEachTime()
        {
            using var provider = new ServiceCollection()
                .AddTransientOracleDapper<LazyOracleContext>(ConnectionString)
                .BuildServiceProvider();

            Assert.NotSame(provider.GetRequiredService<LazyOracleContext>(), provider.GetRequiredService<LazyOracleContext>());
        }

        [Fact]
        public void MultipleContexts_KeepTheirOwnConnectionStrings()
        {
            using var provider = new ServiceCollection()
                .AddScopedOracleDapper<LazyOracleContext>(ConnectionString)
                .AddScopedOracleDapper<OtherOracleContext>("Data Source=other")
                .BuildServiceProvider();

            using var scope = provider.CreateScope();

            Assert.Equal(ConnectionString, scope.ServiceProvider.GetRequiredService<LazyOracleContext>().ConnectionString);
            Assert.Equal("Data Source=other", scope.ServiceProvider.GetRequiredService<OtherOracleContext>().ConnectionString);
        }

        [Fact]
        public void ConnectionStringFactory_IsEvaluatedOnResolve()
        {
            var calls = 0;
            using var provider = new ServiceCollection()
                .AddScopedOracleDapper<LazyOracleContext>(_ => { calls++; return ConnectionString; })
                .BuildServiceProvider();

            Assert.Equal(0, calls);
            using var scope = provider.CreateScope();
            Assert.Equal(ConnectionString, scope.ServiceProvider.GetRequiredService<LazyOracleContext>().ConnectionString);
            Assert.Equal(1, calls);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void EmptyConnectionString_ThrowsNotFoundException(string connectionString)
        {
            using var provider = new ServiceCollection()
                .AddTransientOracleDapper<LazyOracleContext>(connectionString)
                .BuildServiceProvider();

            Assert.Throws<NotFoundException>(() => provider.GetRequiredService<LazyOracleContext>());
        }

        [Fact]
        public void ContextConstructorFailure_SurfacesOriginalException()
        {
            using var provider = new ServiceCollection()
                .AddTransientOracleDapper<FailingContext>(ConnectionString)
                .BuildServiceProvider();

            var exception = Assert.Throws<InvalidOperationException>(() => provider.GetRequiredService<FailingContext>());
            Assert.StartsWith("cannot connect", exception.Message);
        }

        [Fact]
        public void ScopeDisposal_DisposesContext()
        {
            using var provider = new ServiceCollection()
                .AddScopedOracleDapper<LazyOracleContext>(ConnectionString)
                .BuildServiceProvider();

            LazyOracleContext context;
            System.Data.Common.DbConnection scopedConnection;
            using (var scope = provider.CreateScope())
            {
                context = scope.ServiceProvider.GetRequiredService<LazyOracleContext>();
                scopedConnection = context.DbConnection;
            }

            // the scope disposed the context: the underlying connection was released
            Assert.NotSame(scopedConnection, context.DbConnection);
        }
    }
}

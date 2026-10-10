# JM.Oracle.Dapper.NetCore

[![NuGet version](https://img.shields.io/nuget/v/JM.Oracle.Dapper.NetCore)](https://www.nuget.org/packages/JM.Oracle.Dapper.NetCore)
[![NuGet downloads](https://img.shields.io/nuget/dt/JM.Oracle.Dapper.NetCore)](https://www.nuget.org/packages/JM.Oracle.Dapper.NetCore)

Package to simplify the use of Oracle database in .NET projects. It wraps the official Oracle managed driver (ODP.NET Core, `Oracle.ManagedDataAccess.dll`) and the micro ORM [Dapper](https://github.com/DapperLib/Dapper), and gives you:

- a context class ready for dependency injection (scoped or transient);
- connection handling with automatic retry on transient connectivity errors;
- simple transaction control;
- the Dapper API (sync and async) exposed through `context.Database`.

## Contents

- [Supported frameworks](#supported-frameworks)
- [Installation](#installation)
- [Quick start](#quick-start)
- [Connection](#connection)
- [Transactions](#transactions)
- [Database commands](#database-commands)
- [Examples](#examples)
- [Lifetimes and thread safety](#lifetimes-and-thread-safety)
- [Troubleshooting](#troubleshooting)
- [Migrating from 2.x to 3.0](#migrating-from-2x-to-30)
- [Building and testing](#building-and-testing)
- [License](#license)

## Supported frameworks

| Framework | Supported |
| --- | --- |
| .NET 10 | yes |
| .NET 9 | yes |
| .NET 8 | yes |
| .NET 7 | yes |
| .NET 6 | yes |
| .NET 5 | yes |
| .NET Core 3.1 | yes |

Dependencies installed with the package:

| Package | Version |
| --- | --- |
| [Oracle.ManagedDataAccess.Core](https://www.nuget.org/packages/Oracle.ManagedDataAccess.Core) | 23.26.301 |
| [Dapper](https://www.nuget.org/packages/Dapper) | 2.1.89 |
| [Dapper.Oracle](https://www.nuget.org/packages/Dapper.Oracle) | 2.0.3 |
| [Polly](https://www.nuget.org/packages/Polly) | 7.2.4 |

You do not need to reference `Oracle.ManagedDataAccess.dll` yourself. No Oracle Client installation is required: the driver is fully managed.

## Installation

```bash
dotnet add package JM.Oracle.Dapper.NetCore
```

## Quick start

### 1. Create your context

Inherit from `OracleContext`. Create one class per database if you work with multiple contexts.

```csharp
using Oracle.Dapper.NetCore.Context;

public class MyDbContext : OracleContext
{
    public MyDbContext(string connectionString)
        : base(connectionString)
    {
    }
}
```

### 2. Configure the connection string

`appsettings.json`:

```json
{
  "ConnectionStrings": {
    "myOracleConnectionString": "User Id=MY_USER;Password=MY_PASSWORD;Data Source=my-host:1521/MY_SERVICE"
  }
}
```

Both Oracle formats work in `Data Source`:

```text
Easy Connect:   my-host:1521/MY_SERVICE
TNS descriptor: (DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=my-host)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=MY_SERVICE)))
```

### 3. Register in dependency injection

```csharp
using Oracle.Dapper.NetCore.DependencyInjection;

// Scoped: one context (one connection) per scope / HTTP request
services.AddScopedOracleDapper<MyDbContext>(configuration.GetConnectionString("myOracleConnectionString"));

// Transient: one context per resolution - suitable for parallel and multi-thread executions
services.AddTransientOracleDapper<MyDbContext>(configuration.GetConnectionString("myOracleConnectionString"));
```

Choose one of them for each context. To read the connection string only when the context is created, use the factory overload:

```csharp
services.AddScopedOracleDapper<MyDbContext>(provider =>
    provider.GetRequiredService<IConfiguration>().GetConnectionString("myOracleConnectionString"));
```

The methods return `IServiceCollection`, so calls can be chained. Multiple contexts are supported:

```csharp
services
    .AddScopedOracleDapper<SalesDbContext>(salesConnectionString)
    .AddScopedOracleDapper<BillingDbContext>(billingConnectionString);
```

### 4. Use it in a repository

```csharp
using System.Collections.Generic;
using System.Threading.Tasks;

public class CustomerRepository
{
    private readonly MyDbContext _context;

    public CustomerRepository(MyDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<string>> GetNamesAsync() =>
        _context.Database.QueryAsync<string>("SELECT NAME FROM CUSTOMER");
}
```

A complete console application is available in [samples/Oracle.Dapper.NetCore.Console.Example](samples/Oracle.Dapper.NetCore.Console.Example).

## Connection

The connection is opened when the context is created. You normally do not need to open or close it: the context is disposed (and the connection returned to the pool) by the dependency injection container.

Methods are available for manual control:

```csharp
// Opens with the retry policy: up to 3 new attempts, 1 second apart, on transient errors
_context.OpenOracleConnection(errorsExpected: true);

_context.Open();
_context.Close();
```

Errors that trigger a new attempt in `OpenOracleConnection`:

| Error | Meaning |
| --- | --- |
| pool timeout (`-1000`, ORA-50000) | no connection available in the pool |
| ORA-03113, ORA-03114, ORA-03135 | session dropped / not connected |
| ORA-12170, ORA-12537, ORA-12571 | network timeout or closed connection |
| ORA-12514, ORA-12528, ORA-12541, ORA-12545 | listener or service not available |

The driver reports network failures as `ORA-50201` and keeps the real cause (for example `ORA-12541`) in the inner exception. The retry policy inspects the inner exceptions too.

Other errors (invalid user, invalid connect string, invalid SQL, constraint violation) are thrown immediately.

## Transactions

```csharp
_context.BeginTransaction();
_context.BeginTransaction(IsolationLevel.Serializable);
_context.CommitTransaction();
_context.RollbackTransaction();

// Current transaction, or null when no transaction is active
_context.CurrentTransaction
```

Pass the transaction to the commands that belong to it with `_context.CurrentTransaction.Instance`:

```csharp
_context.BeginTransaction();
try
{
    _context.Database.Execute(
        "UPDATE CUSTOMER SET NAME = :NAME WHERE ID = :ID",
        new { NAME = "John", ID = 10 },
        _context.CurrentTransaction.Instance);

    _context.CommitTransaction();
}
catch
{
    _context.RollbackTransaction();
    throw;
}
```

Rules:

- Only one transaction can be active per context. `BeginTransaction` throws `InvalidOperationException` if another transaction is active.
- After `CommitTransaction` or `RollbackTransaction`, `CurrentTransaction` is `null` and a new transaction can be started on the same context.
- If the commit fails, the transaction stays active so it can be rolled back.
- The connection stays open after the transaction finishes.

## Database commands

`_context.Database` exposes the Dapper commands, synchronous and asynchronous:

| Methods in `Database` |
| --- |
| `Execute` / `ExecuteAsync` |
| `ExecuteReader` / `ExecuteReaderAsync` |
| `ExecuteReaderWithReturnDbDataReaderAsync` |
| `ExecuteScalar` / `ExecuteScalarAsync` |
| `Query` / `QueryAsync` (including multi-mapping overloads, up to 7 types) |
| `QueryFirst` / `QueryFirstAsync` |
| `QueryFirstOrDefault` / `QueryFirstOrDefaultAsync` |
| `QueryMultiple` / `QueryMultipleAsync` |
| `QuerySingle` / `QuerySingleAsync` |
| `QuerySingleOrDefault` / `QuerySingleOrDefaultAsync` |

The signatures are the same as the Dapper extension methods. The underlying ADO.NET connection is available in `_context.DbConnection`.

## Examples

Namespaces used below:

```csharp
using System.Data;
using Dapper.Oracle;
```

### Simple query

```csharp
IEnumerable<string> names = _context.Database.Query<string>("SELECT NAME FROM CUSTOMER");
```

### Query with parameters

Oracle bind variables use the `:NAME` syntax.

```csharp
var parameters = new OracleDynamicParameters();
parameters.Add("STATUS", "ACTIVE", OracleMappingType.Varchar2);

IEnumerable<Customer> customers = await _context.Database.QueryAsync<Customer>(
    "SELECT ID, NAME FROM CUSTOMER WHERE STATUS = :STATUS",
    parameters);
```

Anonymous objects also work for simple types:

```csharp
var customer = await _context.Database.QueryFirstOrDefaultAsync<Customer>(
    "SELECT ID, NAME FROM CUSTOMER WHERE ID = :ID",
    new { ID = 10 });
```

### Stored procedure returning a cursor, inside a transaction

```csharp
var parameters = new OracleDynamicParameters();
parameters.Add("P_CUSTOMER_ID", 10, OracleMappingType.Int32);
parameters.Add("P_RESULT", null, OracleMappingType.RefCursor, ParameterDirection.Output);

_context.BeginTransaction();
try
{
    var orders = _context.Database.Query<OrderDto>(
        "PKG_ORDER.LIST_BY_CUSTOMER",
        parameters,
        _context.CurrentTransaction.Instance,
        commandType: CommandType.StoredProcedure);

    _context.CommitTransaction();
}
catch
{
    _context.RollbackTransaction();
    throw;
}
```

### Insert, update and delete

```csharp
int affectedRows = await _context.Database.ExecuteAsync(
    "INSERT INTO CUSTOMER (ID, NAME) VALUES (:ID, :NAME)",
    new { ID = 10, NAME = "John" });
```

### Multi-mapping (join)

```csharp
var orders = _context.Database.Query<Order, Customer, Order>(
    @"SELECT O.ID, O.TOTAL, C.ID, C.NAME
        FROM ORDERS O
        JOIN CUSTOMER C ON C.ID = O.CUSTOMER_ID",
    (order, customer) =>
    {
        order.Customer = customer;
        return order;
    },
    splitOn: "ID");
```

## Lifetimes and thread safety

- A context holds one Oracle connection. A connection cannot run commands in parallel, so **do not share one context between threads**.
- **Scoped** fits web applications: one context per request.
- **Transient** fits background jobs and parallel processing: each consumer receives its own context. Resolve it from a scope (`IServiceScopeFactory.CreateScope()`), so the context is disposed when the scope ends. Transient contexts resolved from the root provider are only disposed when the application stops.
- Do not inject a scoped context into a singleton service.
- `IOracleDbConnection` and `IRelationalConnection` are also registered and resolve to the first registered context.
- Connections are pooled by the Oracle driver. Disposing a context returns its connection to the pool.

## Troubleshooting

| Symptom | Cause / fix |
| --- | --- |
| `NotFoundException: Oracle connection string not found` | The connection string passed to `AddScopedOracleDapper` / `AddTransientOracleDapper` is null or empty. Check the name in `GetConnectionString`. |
| `ORA-50201` | Generic connection failure. Read the inner exception for the real cause (`ORA-12541`, `ORA-12154`, ...). |
| `ORA-12541` / `ORA-12545` | Host or port is wrong, or the listener is down. |
| `ORA-12154` / `ORA-12514` | Service name is wrong. Prefer the Easy Connect format `host:port/service`. |
| `ORA-01017` | Invalid user or password. |
| Name resolution errors with a TNS alias | A TNS alias needs a `tnsnames.ora` file. Use Easy Connect or the full TNS descriptor instead. |
| `InvalidOperationException` about an active transaction | A transaction is already active on this context. Commit or roll back before `BeginTransaction`. |
| Exception when the context is resolved from DI | The connection is opened in the context constructor, so connection errors appear at resolution time. The original `OracleException` is thrown. |

## Migrating from 2.x to 3.0

- **New frameworks:** .NET 8, 9 and 10 were added. All frameworks of 2.x are still supported.
- **Oracle driver:** `Oracle.ManagedDataAccess.Core` changed from `2.12.0-beta3` to `23.26.301` (official ODP.NET Core release). Remove any direct reference to an older version from your project.
- **Dependency injection:** `AddScopedOracleDapper` and `AddTransientOracleDapper` now return `IServiceCollection` instead of `void`. Source code keeps compiling; recompile projects that use the package.
- **`IOracleDbConnection` / `IRelationalConnection`:** now resolve to the registered context. In 2.x they resolved to a connection without connection string.
- **Transactions:** several transactions can now run in sequence on the same context. In 2.x the second `BeginTransaction` failed.
- **Errors on context creation:** the original exception (for example `OracleException`) is thrown. In 2.x it came wrapped in `TargetInvocationException`.
- **Retry:** `OpenOracleConnection` now retries on the transient errors listed in [Connection](#connection).
- **Fix:** `Database.QueryFirstOrDefault<T>(sql, ...)` now maps directly to `T`.

## Building and testing

```bash
dotnet build Oracle.Dapper.NetCore.sln -c Release
```

```bash
dotnet test Oracle.Dapper.NetCore.sln
```

The unit tests do not need a database. To run the sample against a real Oracle database, set the connection string and run it:

```bash
dotnet run --project samples/Oracle.Dapper.NetCore.Console.Example
```

The sample reads `ConnectionStrings:myOracleConnectionString` from `appsettings.json` or from the environment variable `ConnectionStrings__myOracleConnectionString`.

## License

See [LICENSE](licenses/LICENSE.txt).

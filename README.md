# Oracle.Dapper.NetCore
Package to simplify the use of Oracle database in .Net Core projects. This package already implements the use of the micro ORM Dapper to make it even easier.

# Getting Started
## Nuget Package
[![Nuget](https://img.shields.io/nuget/dt/JM.Oracle.Dapper.NetCore)](https://www.nuget.org/packages/JM.Oracle.Dapper.NetCore)  

## (1) Create class inheriting from "Oracle Context" if working with multiple contexts.
```
using Oracle.Dapper.NetCore.Context;

namespace Oracle.Dapper.NetCore.Console.Example.Infra
{
    public class MyDbContext : OracleContext
    {
        public MyDbContext(string connectionString)
            : base(connectionString)
        {
        }
    }
}
```
## (2) Dependency Injection
Register oracle dependencies as scoped
```
services.AddScopedOracleDapper<MyDbContext>("<<CONNECTION_STRING>>");
```
Register oracle dependencies as transient - suitable for parallel and multi-thread executions
```
services.AddTransientOracleDapper<MyDbContext>("<<CONNECTION_STRING>>");
```

## (3) Features
In the context, there are several resources for executing commands in the database, each one of them will be detailed below.
### 3.1) Connection
By default the connection is opened when instantiating the context, however there are methods below for opening and closing the connection.
```
//It has a policy that will make up to 3 attempts to connect to the database if necessary
_context.OpenOracleConnection();
```
```
_context.Open();
```
```
_context.Close();
```
### 3.2) Transactions 
There are below methods for transaction control.
```
_context.BeginTransaction();
```
```
_context.CommitTransaction();
```
```
_context.RollbackTransaction();
```
```
//Get the current transaction (if it exists)
_context.CurrentTransaction
```
### 3.1) Database Commands 
Contains available resources (synchronous and asynchronous) for executing commands in the database
```
_context.Database.
```
* **_context** is a instance of OracleContext
* **Database** is the property that makes resources available for executing commands in the database
 
| list of methods present in [Database]  |
| ------------- |
| Execute | 
| ExecuteAsync | 
| ExecuteReader | 
| ExecuteReaderAsync | 
| ExecuteReaderWithReturnDbDataReaderAsync | 
| ExecuteScalar | 
| ExecuteScalarAsync | 
| Query | 
| QueryAsync | 
| QueryFirst | 
| QueryFirstAsync | 
| QueryFirstOrDefault | 
| QueryFirstOrDefaultAsync | 
| QueryMultiple | 
| QueryMultipleAsync | 
| QuerySingle | 
| QuerySingleAsync | 
| QuerySingleOrDefault | 
| QuerySingleOrDefaultAsync | 

## (3) Examples of use
### A) Simple query execution
```
_context.Database.Query<string>("<sql query>");
```
### B) Execution query with parameters

```
var parameters = new OracleDynamicParameters();
parameters.Add("PARAM_NAME", "VALUE OF PARAM", OracleMappingType.Varchar2);
return await _context.Database.QueryAsync<string>("<sql query>", parameters);
```
### C) Procedure execution with transaction 
```
var parameters = new OracleDynamicParameters();
parameters.Add("PARAM1", valueOfParam, OracleMappingType.Int32);
parameters.Add("OUTPUT_PARAM", null, OracleMappingType.RefCursor, ParameterDirection.Output);
_context.BeginTransaction();
var result = _context.Database.Query<CLASS_DTO>("PROCEDURE_NAME", parameters, _context.CurrentTransaction.Instance, commandType: CommandType.StoredProcedure);
_context.CommitTransaction();
```
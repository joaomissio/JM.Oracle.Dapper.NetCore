using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Oracle.Dapper.NetCore.Dapper
{
    public interface IDapperFeatures
    {
        //
        // Summary:
        //     Execute parameterized SQL.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for this query.
        //
        //   param:
        //     The parameters to use for this query.
        //
        //   transaction:
        //     The transaction to use for this query.
        //
        //   commandTimeout:
        //     Number of seconds before command execution timeout.
        //
        //   commandType:
        //     Is it a stored proc or a batch?
        //
        // Returns:
        //     The number of rows affected.
        int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Execute parameterized SQL.
        //
        // Parameters:
        //   command:
        //     The command to execute on this connection.
        //
        // Returns:
        //     The number of rows affected.
        int Execute(CommandDefinition command);
        //
        // Summary:
        //     Execute a command asynchronously using Task.
        //
        // Parameters:
        //   command:
        //     The command to execute on this connection.
        //
        // Returns:
        //     The number of rows affected.
        Task<int> ExecuteAsync(CommandDefinition command);
        //
        // Summary:
        //     Execute a command asynchronously using Task.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for this query.
        //
        //   param:
        //     The parameters to use for this query.
        //
        //   transaction:
        //     The transaction to use for this query.
        //
        //   commandTimeout:
        //     Number of seconds before command execution timeout.
        //
        //   commandType:
        //     Is it a stored proc or a batch?
        //
        // Returns:
        //     The number of rows affected.
        Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Execute parameterized SQL and return an System.Data.IDataReader.
        //
        // Parameters:
        //   command:
        //     The command to execute.
        //
        // Returns:
        //     An System.Data.IDataReader that can be used to iterate over the results of the
        //     SQL query.
        //
        // Remarks:
        //     This is typically used when the results of a query are not processed by Dapper,
        //     for example, used to fill a System.Data.DataTable or DataSet.
        IDataReader ExecuteReader(CommandDefinition command);
        //
        // Summary:
        //     Execute parameterized SQL and return an System.Data.IDataReader.
        //
        // Parameters:
        //   command:
        //     The command to execute.
        //
        //   commandBehavior:
        //     The System.Data.CommandBehavior flags for this reader.
        //
        // Returns:
        //     An System.Data.IDataReader that can be used to iterate over the results of the
        //     SQL query.
        //
        // Remarks:
        //     This is typically used when the results of a query are not processed by Dapper,
        //     for example, used to fill a System.Data.DataTable or DataSet.
        IDataReader ExecuteReader(CommandDefinition command, CommandBehavior commandBehavior);
        //
        // Summary:
        //     Execute parameterized SQL and return an System.Data.IDataReader.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute.
        //
        //   param:
        //     The parameters to use for this command.
        //
        //   transaction:
        //     The transaction to use for this command.
        //
        //   commandTimeout:
        //     Number of seconds before command execution timeout.
        //
        //   commandType:
        //     Is it a stored proc or a batch?
        //
        // Returns:
        //     An System.Data.IDataReader that can be used to iterate over the results of the
        //     SQL query.
        //
        // Remarks:
        //     This is typically used when the results of a query are not processed by Dapper,
        //     for example, used to fill a System.Data.DataTable or DataSet.
        IDataReader ExecuteReader(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Execute parameterized SQL and return an System.Data.IDataReader.
        //
        // Parameters:
        //   command:
        //     The command to execute.
        //
        //   commandBehavior:
        //     The System.Data.CommandBehavior flags for this reader.
        //
        // Returns:
        //     An System.Data.IDataReader that can be used to iterate over the results of the
        //     SQL query.
        //
        // Remarks:
        //     This is typically used when the results of a query are not processed by Dapper,
        //     for example, used to fill a System.Data.DataTable or DataSet.
        Task<IDataReader> ExecuteReaderAsync(CommandDefinition command, CommandBehavior commandBehavior);
        //
        // Summary:
        //     Execute parameterized SQL and return a System.Data.Common.DbDataReader.
        //
        // Parameters:
        //   command:
        //     The command to execute.
        Task<DbDataReader> ExecuteReaderWithReturnDbDataReaderAsync(CommandDefinition command);
        //
        // Summary:
        //     Execute parameterized SQL and return an System.Data.IDataReader.
        //
        // Parameters:
        //   command:
        //     The command to execute.
        //
        // Returns:
        //     An System.Data.IDataReader that can be used to iterate over the results of the
        //     SQL query.
        //
        // Remarks:
        //     This is typically used when the results of a query are not processed by Dapper,
        //     for example, used to fill a System.Data.DataTable or DataSet.
        Task<IDataReader> ExecuteReaderAsync(CommandDefinition command);
        //
        // Summary:
        //     Execute parameterized SQL and return a System.Data.Common.DbDataReader.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute.
        //
        //   param:
        //     The parameters to use for this command.
        //
        //   transaction:
        //     The transaction to use for this command.
        //
        //   commandTimeout:
        //     Number of seconds before command execution timeout.
        //
        //   commandType:
        //     Is it a stored proc or a batch?
        Task<DbDataReader> ExecuteReaderWithReturnDbDataReaderAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Execute parameterized SQL and return an System.Data.IDataReader.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute.
        //
        //   param:
        //     The parameters to use for this command.
        //
        //   transaction:
        //     The transaction to use for this command.
        //
        //   commandTimeout:
        //     Number of seconds before command execution timeout.
        //
        //   commandType:
        //     Is it a stored proc or a batch?
        //
        // Returns:
        //     An System.Data.IDataReader that can be used to iterate over the results of the
        //     SQL query.
        //
        // Remarks:
        //     This is typically used when the results of a query are not processed by Dapper,
        //     for example, used to fill a System.Data.DataTable or DataSet.
        Task<IDataReader> ExecuteReaderAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Execute parameterized SQL and return a System.Data.Common.DbDataReader.
        //
        // Parameters:
        //   command:
        //     The command to execute.
        //
        //   commandBehavior:
        //     The System.Data.CommandBehavior flags for this reader.
        Task<DbDataReader> ExecuteReaderWithReturnDbDataReaderAsync(CommandDefinition command, CommandBehavior commandBehavior);
        //
        // Summary:
        //     Execute parameterized SQL that selects a single value.
        //
        // Parameters:
        //   command:
        //     The command to execute.
        //
        // Type parameters:
        //   T:
        //     The type to return.
        //
        // Returns:
        //     The first cell selected as T.
        T ExecuteScalar<T>(CommandDefinition command);
        //
        // Summary:
        //     Execute parameterized SQL that selects a single value.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute.
        //
        //   param:
        //     The parameters to use for this command.
        //
        //   transaction:
        //     The transaction to use for this command.
        //
        //   commandTimeout:
        //     Number of seconds before command execution timeout.
        //
        //   commandType:
        //     Is it a stored proc or a batch?
        //
        // Returns:
        //     The first cell selected as System.Object.
        object ExecuteScalar(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Execute parameterized SQL that selects a single value.
        //
        // Parameters:
        //   command:
        //     The command to execute.
        //
        // Returns:
        //     The first cell selected as System.Object.
        object ExecuteScalar(CommandDefinition command);
        //
        // Summary:
        //     Execute parameterized SQL that selects a single value.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute.
        //
        //   param:
        //     The parameters to use for this command.
        //
        //   transaction:
        //     The transaction to use for this command.
        //
        //   commandTimeout:
        //     Number of seconds before command execution timeout.
        //
        //   commandType:
        //     Is it a stored proc or a batch?
        //
        // Type parameters:
        //   T:
        //     The type to return.
        //
        // Returns:
        //     The first cell returned, as T.
        T ExecuteScalar<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Execute parameterized SQL that selects a single value.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute.
        //
        //   param:
        //     The parameters to use for this command.
        //
        //   transaction:
        //     The transaction to use for this command.
        //
        //   commandTimeout:
        //     Number of seconds before command execution timeout.
        //
        //   commandType:
        //     Is it a stored proc or a batch?
        //
        // Returns:
        //     The first cell returned, as System.Object.
        Task<object> ExecuteScalarAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Execute parameterized SQL that selects a single value.
        //
        // Parameters:
        //   command:
        //     The command to execute.
        //
        // Type parameters:
        //   T:
        //     The type to return.
        //
        // Returns:
        //     The first cell selected as T.
        Task<T> ExecuteScalarAsync<T>(CommandDefinition command);
        //
        // Summary:
        //     Execute parameterized SQL that selects a single value.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute.
        //
        //   param:
        //     The parameters to use for this command.
        //
        //   transaction:
        //     The transaction to use for this command.
        //
        //   commandTimeout:
        //     Number of seconds before command execution timeout.
        //
        //   commandType:
        //     Is it a stored proc or a batch?
        //
        // Type parameters:
        //   T:
        //     The type to return.
        //
        // Returns:
        //     The first cell returned, as T.
        Task<T> ExecuteScalarAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Execute parameterized SQL that selects a single value.
        //
        // Parameters:
        //   command:
        //     The command to execute.
        //
        // Returns:
        //     The first cell selected as System.Object.
        Task<object> ExecuteScalarAsync(CommandDefinition command);

        //
        // Summary:
        //     Perform a multi-mapping query with 6 input types. This returns a single type,
        //     combined from the raw types via map.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for this query.
        //
        //   map:
        //     The function to map row types to the return type.
        //
        //   param:
        //     The parameters to use for this query.
        //
        //   transaction:
        //     The transaction to use for this query.
        //
        //   buffered:
        //     Whether to buffer the results in memory.
        //
        //   splitOn:
        //     The field we should split and read the second object from (default: "Id").
        //
        //   commandTimeout:
        //     Number of seconds before command execution timeout.
        //
        //   commandType:
        //     Is it a stored proc or a batch?
        //
        // Type parameters:
        //   TFirst:
        //     The first type in the recordset.
        //
        //   TSecond:
        //     The second type in the recordset.
        //
        //   TThird:
        //     The third type in the recordset.
        //
        //   TFourth:
        //     The fourth type in the recordset.
        //
        //   TFifth:
        //     The fifth type in the recordset.
        //
        //   TSixth:
        //     The sixth type in the recordset.
        //
        //   TReturn:
        //     The combined type to return.
        //
        // Returns:
        //     An enumerable of TReturn.
        IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Executes a query, returning the data typed as T.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for the query.
        //
        //   param:
        //     The parameters to pass, if any.
        //
        //   transaction:
        //     The transaction to use, if any.
        //
        //   buffered:
        //     Whether to buffer results in memory.
        //
        //   commandTimeout:
        //     The command timeout (in seconds).
        //
        //   commandType:
        //     The type of command to execute.
        //
        // Type parameters:
        //   T:
        //     The type of results to return.
        //
        // Returns:
        //     A sequence of data of the supplied type; if a basic type (int, string, etc) is
        //     queried then the data from the first column is assumed, otherwise an instance
        //     is created per row, and a direct column-name===member-name mapping is assumed
        //     (case insensitive).
        IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Perform a multi-mapping query with 5 input types. This returns a single type,
        //     combined from the raw types via map.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for this query.
        //
        //   map:
        //     The function to map row types to the return type.
        //
        //   param:
        //     The parameters to use for this query.
        //
        //   transaction:
        //     The transaction to use for this query.
        //
        //   buffered:
        //     Whether to buffer the results in memory.
        //
        //   splitOn:
        //     The field we should split and read the second object from (default: "Id").
        //
        //   commandTimeout:
        //     Number of seconds before command execution timeout.
        //
        //   commandType:
        //     Is it a stored proc or a batch?
        //
        // Type parameters:
        //   TFirst:
        //     The first type in the recordset.
        //
        //   TSecond:
        //     The second type in the recordset.
        //
        //   TThird:
        //     The third type in the recordset.
        //
        //   TFourth:
        //     The fourth type in the recordset.
        //
        //   TFifth:
        //     The fifth type in the recordset.
        //
        //   TReturn:
        //     The combined type to return.
        //
        // Returns:
        //     An enumerable of TReturn.
        IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Return a sequence of dynamic objects with properties matching the columns.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for the query.
        //
        //   param:
        //     The parameters to pass, if any.
        //
        //   transaction:
        //     The transaction to use, if any.
        //
        //   buffered:
        //     Whether to buffer the results in memory.
        //
        //   commandTimeout:
        //     The command timeout (in seconds).
        //
        //   commandType:
        //     The type of command to execute.
        //
        // Remarks:
        //     Note: each row can be accessed via "dynamic", or by casting to an IDictionary<string,object>
        IEnumerable<dynamic> Query(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Perform a multi-mapping query with 4 input types. This returns a single type,
        //     combined from the raw types via map.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for this query.
        //
        //   map:
        //     The function to map row types to the return type.
        //
        //   param:
        //     The parameters to use for this query.
        //
        //   transaction:
        //     The transaction to use for this query.
        //
        //   buffered:
        //     Whether to buffer the results in memory.
        //
        //   splitOn:
        //     The field we should split and read the second object from (default: "Id").
        //
        //   commandTimeout:
        //     Number of seconds before command execution timeout.
        //
        //   commandType:
        //     Is it a stored proc or a batch?
        //
        // Type parameters:
        //   TFirst:
        //     The first type in the recordset.
        //
        //   TSecond:
        //     The second type in the recordset.
        //
        //   TThird:
        //     The third type in the recordset.
        //
        //   TFourth:
        //     The fourth type in the recordset.
        //
        //   TReturn:
        //     The combined type to return.
        //
        // Returns:
        //     An enumerable of TReturn.
        IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Perform a multi-mapping query with 3 input types. This returns a single type,
        //     combined from the raw types via map.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for this query.
        //
        //   map:
        //     The function to map row types to the return type.
        //
        //   param:
        //     The parameters to use for this query.
        //
        //   transaction:
        //     The transaction to use for this query.
        //
        //   buffered:
        //     Whether to buffer the results in memory.
        //
        //   splitOn:
        //     The field we should split and read the second object from (default: "Id").
        //
        //   commandTimeout:
        //     Number of seconds before command execution timeout.
        //
        //   commandType:
        //     Is it a stored proc or a batch?
        //
        // Type parameters:
        //   TFirst:
        //     The first type in the recordset.
        //
        //   TSecond:
        //     The second type in the recordset.
        //
        //   TThird:
        //     The third type in the recordset.
        //
        //   TReturn:
        //     The combined type to return.
        //
        // Returns:
        //     An enumerable of TReturn.
        IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Executes a query, returning the data typed as T.
        //
        // Parameters:
        //   command:
        //     The command used to query on this connection.
        //
        // Type parameters:
        //   T:
        //     The type of results to return.
        //
        // Returns:
        //     A sequence of data of T; if a basic type (int, string, etc) is queried then the
        //     data from the first column is assumed, otherwise an instance is created per row,
        //     and a direct column-name===member-name mapping is assumed (case insensitive).
        IEnumerable<T> Query<T>(CommandDefinition command);
        //
        // Summary:
        //     Executes a single-row query, returning the data typed as type.
        //
        // Parameters:
        //   type:
        //     The type to return.
        //
        //   sql:
        //     The SQL to execute for the query.
        //
        //   param:
        //     The parameters to pass, if any.
        //
        //   transaction:
        //     The transaction to use, if any.
        //
        //   buffered:
        //     Whether to buffer results in memory.
        //
        //   commandTimeout:
        //     The command timeout (in seconds).
        //
        //   commandType:
        //     The type of command to execute.
        //
        // Returns:
        //     A sequence of data of the supplied type; if a basic type (int, string, etc) is
        //     queried then the data from the first column is assumed, otherwise an instance
        //     is created per row, and a direct column-name===member-name mapping is assumed
        //     (case insensitive).
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     type is null.
        IEnumerable<object> Query(Type type, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Perform a multi-mapping query with 2 input types. This returns a single type,
        //     combined from the raw types via map.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for this query.
        //
        //   map:
        //     The function to map row types to the return type.
        //
        //   param:
        //     The parameters to use for this query.
        //
        //   transaction:
        //     The transaction to use for this query.
        //
        //   buffered:
        //     Whether to buffer the results in memory.
        //
        //   splitOn:
        //     The field we should split and read the second object from (default: "Id").
        //
        //   commandTimeout:
        //     Number of seconds before command execution timeout.
        //
        //   commandType:
        //     Is it a stored proc or a batch?
        //
        // Type parameters:
        //   TFirst:
        //     The first type in the recordset.
        //
        //   TSecond:
        //     The second type in the recordset.
        //
        //   TReturn:
        //     The combined type to return.
        //
        // Returns:
        //     An enumerable of TReturn.
        IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Perform a multi-mapping query with an arbitrary number of input types. This returns
        //     a single type, combined from the raw types via map.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for this query.
        //
        //   types:
        //     Array of types in the recordset.
        //
        //   map:
        //     The function to map row types to the return type.
        //
        //   param:
        //     The parameters to use for this query.
        //
        //   transaction:
        //     The transaction to use for this query.
        //
        //   buffered:
        //     Whether to buffer the results in memory.
        //
        //   splitOn:
        //     The field we should split and read the second object from (default: "Id").
        //
        //   commandTimeout:
        //     Number of seconds before command execution timeout.
        //
        //   commandType:
        //     Is it a stored proc or a batch?
        //
        // Type parameters:
        //   TReturn:
        //     The combined type to return.
        //
        // Returns:
        //     An enumerable of TReturn.
        IEnumerable<TReturn> Query<TReturn>(string sql, Type[] types, Func<object[], TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Perform a multi-mapping query with 7 input types. If you need more types -> use
        //     Query with Type[] parameter. This returns a single type, combined from the raw
        //     types via map.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for this query.
        //
        //   map:
        //     The function to map row types to the return type.
        //
        //   param:
        //     The parameters to use for this query.
        //
        //   transaction:
        //     The transaction to use for this query.
        //
        //   buffered:
        //     Whether to buffer the results in memory.
        //
        //   splitOn:
        //     The field we should split and read the second object from (default: "Id").
        //
        //   commandTimeout:
        //     Number of seconds before command execution timeout.
        //
        //   commandType:
        //     Is it a stored proc or a batch?
        //
        // Type parameters:
        //   TFirst:
        //     The first type in the recordset.
        //
        //   TSecond:
        //     The second type in the recordset.
        //
        //   TThird:
        //     The third type in the recordset.
        //
        //   TFourth:
        //     The fourth type in the recordset.
        //
        //   TFifth:
        //     The fifth type in the recordset.
        //
        //   TSixth:
        //     The sixth type in the recordset.
        //
        //   TSeventh:
        //     The seventh type in the recordset.
        //
        //   TReturn:
        //     The combined type to return.
        //
        // Returns:
        //     An enumerable of TReturn.
        IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Execute a query asynchronously using Task.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for the query.
        //
        //   param:
        //     The parameters to pass, if any.
        //
        //   transaction:
        //     The transaction to use, if any.
        //
        //   commandTimeout:
        //     The command timeout (in seconds).
        //
        //   commandType:
        //     The type of command to execute.
        //
        // Type parameters:
        //   T:
        //     The type of results to return.
        //
        // Returns:
        //     A sequence of data of T; if a basic type (int, string, etc) is queried then the
        //     data from the first column in assumed, otherwise an instance is created per row,
        //     and a direct column-name===member-name mapping is assumed (case insensitive).
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Execute a query asynchronously using Task.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for the query.
        //
        //   param:
        //     The parameters to pass, if any.
        //
        //   transaction:
        //     The transaction to use, if any.
        //
        //   commandTimeout:
        //     The command timeout (in seconds).
        //
        //   commandType:
        //     The type of command to execute.
        //
        // Remarks:
        //     Note: each row can be accessed via "dynamic", or by casting to an IDictionary<string,object>
        Task<IEnumerable<dynamic>> QueryAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Perform an asynchronous multi-mapping query with an arbitrary number of input
        //     types. This returns a single type, combined from the raw types via map.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for this query.
        //
        //   types:
        //     Array of types in the recordset.
        //
        //   map:
        //     The function to map row types to the return type.
        //
        //   param:
        //     The parameters to use for this query.
        //
        //   transaction:
        //     The transaction to use for this query.
        //
        //   buffered:
        //     Whether to buffer the results in memory.
        //
        //   splitOn:
        //     The field we should split and read the second object from (default: "Id").
        //
        //   commandTimeout:
        //     Number of seconds before command execution timeout.
        //
        //   commandType:
        //     Is it a stored proc or a batch?
        //
        // Type parameters:
        //   TReturn:
        //     The combined type to return.
        //
        // Returns:
        //     An enumerable of TReturn.
        Task<IEnumerable<TReturn>> QueryAsync<TReturn>(string sql, Type[] types, Func<object[], TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Execute a query asynchronously using Task.
        //
        // Parameters:
        //   type:
        //     The type to return.
        //
        //   command:
        //     The command used to query on this connection.
        Task<IEnumerable<object>> QueryAsync(Type type, CommandDefinition command);
        //
        // Summary:
        //     Execute a query asynchronously using Task.
        //
        // Parameters:
        //   command:
        //     The command used to query on this connection.
        //
        // Type parameters:
        //   T:
        //     The type to return.
        //
        // Returns:
        //     A sequence of data of T; if a basic type (int, string, etc) is queried then the
        //     data from the first column in assumed, otherwise an instance is created per row,
        //     and a direct column-name===member-name mapping is assumed (case insensitive).
        Task<IEnumerable<T>> QueryAsync<T>(CommandDefinition command);
        //
        // Summary:
        //     Perform an asynchronous multi-mapping query with 2 input types. This returns
        //     a single type, combined from the raw types via map.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for this query.
        //
        //   map:
        //     The function to map row types to the return type.
        //
        //   param:
        //     The parameters to use for this query.
        //
        //   transaction:
        //     The transaction to use for this query.
        //
        //   buffered:
        //     Whether to buffer the results in memory.
        //
        //   splitOn:
        //     The field we should split and read the second object from (default: "Id").
        //
        //   commandTimeout:
        //     Number of seconds before command execution timeout.
        //
        //   commandType:
        //     Is it a stored proc or a batch?
        //
        // Type parameters:
        //   TFirst:
        //     The first type in the recordset.
        //
        //   TSecond:
        //     The second type in the recordset.
        //
        //   TReturn:
        //     The combined type to return.
        //
        // Returns:
        //     An enumerable of TReturn.
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Perform an asynchronous multi-mapping query with 2 input types. This returns
        //     a single type, combined from the raw types via map.
        //
        // Parameters:
        //   splitOn:
        //     The field we should split and read the second object from (default: "Id").
        //
        //   command:
        //     The command to execute.
        //
        //   map:
        //     The function to map row types to the return type.
        //
        // Type parameters:
        //   TFirst:
        //     The first type in the recordset.
        //
        //   TSecond:
        //     The second type in the recordset.
        //
        //   TReturn:
        //     The combined type to return.
        //
        // Returns:
        //     An enumerable of TReturn.
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(CommandDefinition command, Func<TFirst, TSecond, TReturn> map, string splitOn = "Id");
        //
        // Summary:
        //     Perform an asynchronous multi-mapping query with 3 input types. This returns
        //     a single type, combined from the raw types via map.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for this query.
        //
        //   map:
        //     The function to map row types to the return type.
        //
        //   param:
        //     The parameters to use for this query.
        //
        //   transaction:
        //     The transaction to use for this query.
        //
        //   buffered:
        //     Whether to buffer the results in memory.
        //
        //   splitOn:
        //     The field we should split and read the second object from (default: "Id").
        //
        //   commandTimeout:
        //     Number of seconds before command execution timeout.
        //
        //   commandType:
        //     Is it a stored proc or a batch?
        //
        // Type parameters:
        //   TFirst:
        //     The first type in the recordset.
        //
        //   TSecond:
        //     The second type in the recordset.
        //
        //   TThird:
        //     The third type in the recordset.
        //
        //   TReturn:
        //     The combined type to return.
        //
        // Returns:
        //     An enumerable of TReturn.
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Perform an asynchronous multi-mapping query with 3 input types. This returns
        //     a single type, combined from the raw types via map.
        //
        // Parameters:
        //   splitOn:
        //     The field we should split and read the second object from (default: "Id").
        //
        //   command:
        //     The command to execute.
        //
        //   map:
        //     The function to map row types to the return type.
        //
        // Type parameters:
        //   TFirst:
        //     The first type in the recordset.
        //
        //   TSecond:
        //     The second type in the recordset.
        //
        //   TThird:
        //     The third type in the recordset.
        //
        //   TReturn:
        //     The combined type to return.
        //
        // Returns:
        //     An enumerable of TReturn.
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(CommandDefinition command, Func<TFirst, TSecond, TThird, TReturn> map, string splitOn = "Id");
        //
        // Summary:
        //     Execute a query asynchronously using Task.
        //
        // Parameters:
        //   type:
        //     The type to return.
        //
        //   sql:
        //     The SQL to execute for the query.
        //
        //   param:
        //     The parameters to pass, if any.
        //
        //   transaction:
        //     The transaction to use, if any.
        //
        //   commandTimeout:
        //     The command timeout (in seconds).
        //
        //   commandType:
        //     The type of command to execute.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     type is null.
        Task<IEnumerable<object>> QueryAsync(Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Perform an asynchronous multi-mapping query with 4 input types. This returns
        //     a single type, combined from the raw types via map.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for this query.
        //
        //   map:
        //     The function to map row types to the return type.
        //
        //   param:
        //     The parameters to use for this query.
        //
        //   transaction:
        //     The transaction to use for this query.
        //
        //   buffered:
        //     Whether to buffer the results in memory.
        //
        //   splitOn:
        //     The field we should split and read the second object from (default: "Id").
        //
        //   commandTimeout:
        //     Number of seconds before command execution timeout.
        //
        //   commandType:
        //     Is it a stored proc or a batch?
        //
        // Type parameters:
        //   TFirst:
        //     The first type in the recordset.
        //
        //   TSecond:
        //     The second type in the recordset.
        //
        //   TThird:
        //     The third type in the recordset.
        //
        //   TFourth:
        //     The fourth type in the recordset.
        //
        //   TReturn:
        //     The combined type to return.
        //
        // Returns:
        //     An enumerable of TReturn.
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Perform an asynchronous multi-mapping query with 5 input types. This returns
        //     a single type, combined from the raw types via map.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for this query.
        //
        //   map:
        //     The function to map row types to the return type.
        //
        //   param:
        //     The parameters to use for this query.
        //
        //   transaction:
        //     The transaction to use for this query.
        //
        //   buffered:
        //     Whether to buffer the results in memory.
        //
        //   splitOn:
        //     The field we should split and read the second object from (default: "Id").
        //
        //   commandTimeout:
        //     Number of seconds before command execution timeout.
        //
        //   commandType:
        //     Is it a stored proc or a batch?
        //
        // Type parameters:
        //   TFirst:
        //     The first type in the recordset.
        //
        //   TSecond:
        //     The second type in the recordset.
        //
        //   TThird:
        //     The third type in the recordset.
        //
        //   TFourth:
        //     The fourth type in the recordset.
        //
        //   TFifth:
        //     The fifth type in the recordset.
        //
        //   TReturn:
        //     The combined type to return.
        //
        // Returns:
        //     An enumerable of TReturn.
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Perform an asynchronous multi-mapping query with 5 input types. This returns
        //     a single type, combined from the raw types via map.
        //
        // Parameters:
        //   splitOn:
        //     The field we should split and read the second object from (default: "Id").
        //
        //   command:
        //     The command to execute.
        //
        //   map:
        //     The function to map row types to the return type.
        //
        // Type parameters:
        //   TFirst:
        //     The first type in the recordset.
        //
        //   TSecond:
        //     The second type in the recordset.
        //
        //   TThird:
        //     The third type in the recordset.
        //
        //   TFourth:
        //     The fourth type in the recordset.
        //
        //   TFifth:
        //     The fifth type in the recordset.
        //
        //   TReturn:
        //     The combined type to return.
        //
        // Returns:
        //     An enumerable of TReturn.
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(CommandDefinition command, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, string splitOn = "Id");
        //
        // Summary:
        //     Perform an asynchronous multi-mapping query with 6 input types. This returns
        //     a single type, combined from the raw types via map.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for this query.
        //
        //   map:
        //     The function to map row types to the return type.
        //
        //   param:
        //     The parameters to use for this query.
        //
        //   transaction:
        //     The transaction to use for this query.
        //
        //   buffered:
        //     Whether to buffer the results in memory.
        //
        //   splitOn:
        //     The field we should split and read the second object from (default: "Id").
        //
        //   commandTimeout:
        //     Number of seconds before command execution timeout.
        //
        //   commandType:
        //     Is it a stored proc or a batch?
        //
        // Type parameters:
        //   TFirst:
        //     The first type in the recordset.
        //
        //   TSecond:
        //     The second type in the recordset.
        //
        //   TThird:
        //     The third type in the recordset.
        //
        //   TFourth:
        //     The fourth type in the recordset.
        //
        //   TFifth:
        //     The fifth type in the recordset.
        //
        //   TSixth:
        //     The sixth type in the recordset.
        //
        //   TReturn:
        //     The combined type to return.
        //
        // Returns:
        //     An enumerable of TReturn.
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Perform an asynchronous multi-mapping query with 6 input types. This returns
        //     a single type, combined from the raw types via map.
        //
        // Parameters:
        //   splitOn:
        //     The field we should split and read the second object from (default: "Id").
        //
        //   command:
        //     The command to execute.
        //
        //   map:
        //     The function to map row types to the return type.
        //
        // Type parameters:
        //   TFirst:
        //     The first type in the recordset.
        //
        //   TSecond:
        //     The second type in the recordset.
        //
        //   TThird:
        //     The third type in the recordset.
        //
        //   TFourth:
        //     The fourth type in the recordset.
        //
        //   TFifth:
        //     The fifth type in the recordset.
        //
        //   TSixth:
        //     The sixth type in the recordset.
        //
        //   TReturn:
        //     The combined type to return.
        //
        // Returns:
        //     An enumerable of TReturn.
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(CommandDefinition command, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, string splitOn = "Id");
        //
        // Summary:
        //     Perform an asynchronous multi-mapping query with 7 input types. This returns
        //     a single type, combined from the raw types via map.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for this query.
        //
        //   map:
        //     The function to map row types to the return type.
        //
        //   param:
        //     The parameters to use for this query.
        //
        //   transaction:
        //     The transaction to use for this query.
        //
        //   buffered:
        //     Whether to buffer the results in memory.
        //
        //   splitOn:
        //     The field we should split and read the second object from (default: "Id").
        //
        //   commandTimeout:
        //     Number of seconds before command execution timeout.
        //
        //   commandType:
        //     Is it a stored proc or a batch?
        //
        // Type parameters:
        //   TFirst:
        //     The first type in the recordset.
        //
        //   TSecond:
        //     The second type in the recordset.
        //
        //   TThird:
        //     The third type in the recordset.
        //
        //   TFourth:
        //     The fourth type in the recordset.
        //
        //   TFifth:
        //     The fifth type in the recordset.
        //
        //   TSixth:
        //     The sixth type in the recordset.
        //
        //   TSeventh:
        //     The seventh type in the recordset.
        //
        //   TReturn:
        //     The combined type to return.
        //
        // Returns:
        //     An enumerable of TReturn.
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Perform an asynchronous multi-mapping query with 7 input types. This returns
        //     a single type, combined from the raw types via map.
        //
        // Parameters:
        //   splitOn:
        //     The field we should split and read the second object from (default: "Id").
        //
        //   command:
        //     The command to execute.
        //
        //   map:
        //     The function to map row types to the return type.
        //
        // Type parameters:
        //   TFirst:
        //     The first type in the recordset.
        //
        //   TSecond:
        //     The second type in the recordset.
        //
        //   TThird:
        //     The third type in the recordset.
        //
        //   TFourth:
        //     The fourth type in the recordset.
        //
        //   TFifth:
        //     The fifth type in the recordset.
        //
        //   TSixth:
        //     The sixth type in the recordset.
        //
        //   TSeventh:
        //     The seventh type in the recordset.
        //
        //   TReturn:
        //     The combined type to return.
        //
        // Returns:
        //     An enumerable of TReturn.
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(CommandDefinition command, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, string splitOn = "Id");
        //
        // Summary:
        //     Perform an asynchronous multi-mapping query with 4 input types. This returns
        //     a single type, combined from the raw types via map.
        //
        // Parameters:
        //   splitOn:
        //     The field we should split and read the second object from (default: "Id").
        //
        //   command:
        //     The command to execute.
        //
        //   map:
        //     The function to map row types to the return type.
        //
        // Type parameters:
        //   TFirst:
        //     The first type in the recordset.
        //
        //   TSecond:
        //     The second type in the recordset.
        //
        //   TThird:
        //     The third type in the recordset.
        //
        //   TFourth:
        //     The fourth type in the recordset.
        //
        //   TReturn:
        //     The combined type to return.
        //
        // Returns:
        //     An enumerable of TReturn.
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(CommandDefinition command, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, string splitOn = "Id");
        //
        // Summary:
        //     Execute a query asynchronously using Task.
        //
        // Parameters:
        //   command:
        //     The command used to query on this connection.
        //
        // Remarks:
        //     Note: each row can be accessed via "dynamic", or by casting to an IDictionary<string,object>
        Task<IEnumerable<dynamic>> QueryAsync(CommandDefinition command);

        object QueryFirst(Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Executes a query, returning the data typed as T.
        //
        // Parameters:
        //   command:
        //     The command used to query on this connection.
        //
        // Type parameters:
        //   T:
        //     The type of results to return.
        //
        // Returns:
        //     A single instance or null of the supplied type; if a basic type (int, string,
        //     etc) is queried then the data from the first column is assumed, otherwise an
        //     instance is created per row, and a direct column-name===member-name mapping is
        //     assumed (case insensitive).
        T QueryFirst<T>(CommandDefinition command);
        //
        // Summary:
        //     Executes a single-row query, returning the data typed as T.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for the query.
        //
        //   param:
        //     The parameters to pass, if any.
        //
        //   transaction:
        //     The transaction to use, if any.
        //
        //   commandTimeout:
        //     The command timeout (in seconds).
        //
        //   commandType:
        //     The type of command to execute.
        //
        // Type parameters:
        //   T:
        //     The type of result to return.
        //
        // Returns:
        //     A sequence of data of the supplied type; if a basic type (int, string, etc) is
        //     queried then the data from the first column is assumed, otherwise an instance
        //     is created per row, and a direct column-name===member-name mapping is assumed
        //     (case insensitive).
        T QueryFirst<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Return a dynamic object with properties matching the columns.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for the query.
        //
        //   param:
        //     The parameters to pass, if any.
        //
        //   transaction:
        //     The transaction to use, if any.
        //
        //   commandTimeout:
        //     The command timeout (in seconds).
        //
        //   commandType:
        //     The type of command to execute.
        //
        // Remarks:
        //     Note: the row can be accessed via "dynamic", or by casting to an IDictionary<string,object>
        dynamic QueryFirst(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Execute a single-row query asynchronously using Task.
        //
        // Parameters:
        //   command:
        //     The command used to query on this connection.
        //
        // Type parameters:
        //   T:
        //     The type to return.
        Task<T> QueryFirstAsync<T>(CommandDefinition command);
        //
        // Summary:
        //     Execute a single-row query asynchronously using Task.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for the query.
        //
        //   param:
        //     The parameters to pass, if any.
        //
        //   transaction:
        //     The transaction to use, if any.
        //
        //   commandTimeout:
        //     The command timeout (in seconds).
        //
        //   commandType:
        //     The type of command to execute.
        Task<dynamic> QueryFirstAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Execute a single-row query asynchronously using Task.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for the query.
        //
        //   param:
        //     The parameters to pass, if any.
        //
        //   transaction:
        //     The transaction to use, if any.
        //
        //   commandTimeout:
        //     The command timeout (in seconds).
        //
        //   commandType:
        //     The type of command to execute.
        //
        // Type parameters:
        //   T:
        //     The type of result to return.
        Task<T> QueryFirstAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Execute a single-row query asynchronously using Task.
        //
        // Parameters:
        //   type:
        //     The type to return.
        //
        //   command:
        //     The command used to query on this connection.
        Task<object> QueryFirstAsync(Type type, CommandDefinition command);
        //
        // Summary:
        //     Execute a single-row query asynchronously using Task.
        //
        // Parameters:
        //   type:
        //     The type to return.
        //
        //   sql:
        //     The SQL to execute for the query.
        //
        //   param:
        //     The parameters to pass, if any.
        //
        //   transaction:
        //     The transaction to use, if any.
        //
        //   commandTimeout:
        //     The command timeout (in seconds).
        //
        //   commandType:
        //     The type of command to execute.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     type is null.
        Task<object> QueryFirstAsync(Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Execute a single-row query asynchronously using Task.
        //
        // Parameters:
        //   command:
        //     The command used to query on this connection.
        //
        // Remarks:
        //     Note: the row can be accessed via "dynamic", or by casting to an IDictionary<string,object>
        Task<dynamic> QueryFirstAsync(CommandDefinition command);
        //
        // Summary:
        //     Executes a query, returning the data typed as T.
        //
        // Parameters:
        //   command:
        //     The command used to query on this connection.
        //
        // Type parameters:
        //   T:
        //     The type of results to return.
        //
        // Returns:
        //     A single or null instance of the supplied type; if a basic type (int, string,
        //     etc) is queried then the data from the first column is assumed, otherwise an
        //     instance is created per row, and a direct column-name===member-name mapping is
        //     assumed (case insensitive).
        T QueryFirstOrDefault<T>(CommandDefinition command);
        //
        // Summary:
        //     Executes a single-row query, returning the data typed as T.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for the query.
        //
        //   param:
        //     The parameters to pass, if any.
        //
        //   transaction:
        //     The transaction to use, if any.
        //
        //   commandTimeout:
        //     The command timeout (in seconds).
        //
        //   commandType:
        //     The type of command to execute.
        //
        // Type parameters:
        //   T:
        //     The type of result to return.
        //
        // Returns:
        //     A sequence of data of the supplied type; if a basic type (int, string, etc) is
        //     queried then the data from the first column is assumed, otherwise an instance
        //     is created per row, and a direct column-name===member-name mapping is assumed
        //     (case insensitive).
        T QueryFirstOrDefault<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Return a dynamic object with properties matching the columns.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for the query.
        //
        //   param:
        //     The parameters to pass, if any.
        //
        //   transaction:
        //     The transaction to use, if any.
        //
        //   commandTimeout:
        //     The command timeout (in seconds).
        //
        //   commandType:
        //     The type of command to execute.
        //
        // Remarks:
        //     Note: the row can be accessed via "dynamic", or by casting to an IDictionary<string,object>
        dynamic QueryFirstOrDefault(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Executes a single-row query, returning the data typed as type.
        //
        // Parameters:
        //   type:
        //     The type to return.
        //
        //   sql:
        //     The SQL to execute for the query.
        //
        //   param:
        //     The parameters to pass, if any.
        //
        //   transaction:
        //     The transaction to use, if any.
        //
        //   commandTimeout:
        //     The command timeout (in seconds).
        //
        //   commandType:
        //     The type of command to execute.
        //
        // Returns:
        //     A sequence of data of the supplied type; if a basic type (int, string, etc) is
        //     queried then the data from the first column is assumed, otherwise an instance
        //     is created per row, and a direct column-name===member-name mapping is assumed
        //     (case insensitive).
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     type is null.
        object QueryFirstOrDefault(Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Execute a single-row query asynchronously using Task.
        //
        // Parameters:
        //   command:
        //     The command used to query on this connection.
        //
        // Remarks:
        //     Note: the row can be accessed via "dynamic", or by casting to an IDictionary<string,object>
        Task<dynamic> QueryFirstOrDefaultAsync(CommandDefinition command);
        //
        // Summary:
        //     Execute a single-row query asynchronously using Task.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for the query.
        //
        //   param:
        //     The parameters to pass, if any.
        //
        //   transaction:
        //     The transaction to use, if any.
        //
        //   commandTimeout:
        //     The command timeout (in seconds).
        //
        //   commandType:
        //     The type of command to execute.
        Task<dynamic> QueryFirstOrDefaultAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Execute a single-row query asynchronously using Task.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for the query.
        //
        //   param:
        //     The parameters to pass, if any.
        //
        //   transaction:
        //     The transaction to use, if any.
        //
        //   commandTimeout:
        //     The command timeout (in seconds).
        //
        //   commandType:
        //     The type of command to execute.
        //
        // Type parameters:
        //   T:
        //     The type of result to return.
        Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Execute a single-row query asynchronously using Task.
        //
        // Parameters:
        //   command:
        //     The command used to query on this connection.
        //
        // Type parameters:
        //   T:
        //     The type to return.
        Task<T> QueryFirstOrDefaultAsync<T>(CommandDefinition command);
        //
        // Summary:
        //     Execute a single-row query asynchronously using Task.
        //
        // Parameters:
        //   type:
        //     The type to return.
        //
        //   command:
        //     The command used to query on this connection.
        Task<object> QueryFirstOrDefaultAsync(Type type, CommandDefinition command);
        //
        // Summary:
        //     Execute a single-row query asynchronously using Task.
        //
        // Parameters:
        //   type:
        //     The type to return.
        //
        //   sql:
        //     The SQL to execute for the query.
        //
        //   param:
        //     The parameters to pass, if any.
        //
        //   transaction:
        //     The transaction to use, if any.
        //
        //   commandTimeout:
        //     The command timeout (in seconds).
        //
        //   commandType:
        //     The type of command to execute.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     type is null.
        Task<object> QueryFirstOrDefaultAsync(Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Execute a command that returns multiple result sets, and access each in turn.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for this query.
        //
        //   param:
        //     The parameters to use for this query.
        //
        //   transaction:
        //     The transaction to use for this query.
        //
        //   commandTimeout:
        //     Number of seconds before command execution timeout.
        //
        //   commandType:
        //     Is it a stored proc or a batch?
        GridReader QueryMultiple(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Execute a command that returns multiple result sets, and access each in turn.
        //
        // Parameters:
        //   command:
        //     The command to execute for this query.
        GridReader QueryMultiple(CommandDefinition command);
        //
        // Summary:
        //     Execute a command that returns multiple result sets, and access each in turn.
        //
        // Parameters:
        //   command:
        //     The command to execute for this query.
        Task<GridReader> QueryMultipleAsync(CommandDefinition command);
        //
        // Summary:
        //     Execute a command that returns multiple result sets, and access each in turn.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for this query.
        //
        //   param:
        //     The parameters to use for this query.
        //
        //   transaction:
        //     The transaction to use for this query.
        //
        //   commandTimeout:
        //     Number of seconds before command execution timeout.
        //
        //   commandType:
        //     Is it a stored proc or a batch?
        Task<GridReader> QueryMultipleAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Executes a single-row query, returning the data typed as type.
        //
        // Parameters:
        //   type:
        //     The type to return.
        //
        //   sql:
        //     The SQL to execute for the query.
        //
        //   param:
        //     The parameters to pass, if any.
        //
        //   transaction:
        //     The transaction to use, if any.
        //
        //   commandTimeout:
        //     The command timeout (in seconds).
        //
        //   commandType:
        //     The type of command to execute.
        //
        // Returns:
        //     A sequence of data of the supplied type; if a basic type (int, string, etc) is
        //     queried then the data from the first column is assumed, otherwise an instance
        //     is created per row, and a direct column-name===member-name mapping is assumed
        //     (case insensitive).
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     type is null.
        object QuerySingle(Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Executes a query, returning the data typed as T.
        //
        // Parameters:
        //   command:
        //     The command used to query on this connection.
        //
        // Type parameters:
        //   T:
        //     The type of results to return.
        //
        // Returns:
        //     A single instance of the supplied type; if a basic type (int, string, etc) is
        //     queried then the data from the first column is assumed, otherwise an instance
        //     is created per row, and a direct column-name===member-name mapping is assumed
        //     (case insensitive).
        T QuerySingle<T>(CommandDefinition command);
        //
        // Summary:
        //     Executes a single-row query, returning the data typed as T.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for the query.
        //
        //   param:
        //     The parameters to pass, if any.
        //
        //   transaction:
        //     The transaction to use, if any.
        //
        //   commandTimeout:
        //     The command timeout (in seconds).
        //
        //   commandType:
        //     The type of command to execute.
        //
        // Type parameters:
        //   T:
        //     The type of result to return.
        //
        // Returns:
        //     A sequence of data of the supplied type; if a basic type (int, string, etc) is
        //     queried then the data from the first column is assumed, otherwise an instance
        //     is created per row, and a direct column-name===member-name mapping is assumed
        //     (case insensitive).
        T QuerySingle<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Return a dynamic object with properties matching the columns.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for the query.
        //
        //   param:
        //     The parameters to pass, if any.
        //
        //   transaction:
        //     The transaction to use, if any.
        //
        //   commandTimeout:
        //     The command timeout (in seconds).
        //
        //   commandType:
        //     The type of command to execute.
        //
        // Remarks:
        //     Note: the row can be accessed via "dynamic", or by casting to an IDictionary<string,object>
        dynamic QuerySingle(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Execute a single-row query asynchronously using Task.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for the query.
        //
        //   param:
        //     The parameters to pass, if any.
        //
        //   transaction:
        //     The transaction to use, if any.
        //
        //   commandTimeout:
        //     The command timeout (in seconds).
        //
        //   commandType:
        //     The type of command to execute.
        Task<dynamic> QuerySingleAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Execute a single-row query asynchronously using Task.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for the query.
        //
        //   param:
        //     The parameters to pass, if any.
        //
        //   transaction:
        //     The transaction to use, if any.
        //
        //   commandTimeout:
        //     The command timeout (in seconds).
        //
        //   commandType:
        //     The type of command to execute.
        //
        // Type parameters:
        //   T:
        //     The type of result to return.
        Task<T> QuerySingleAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Execute a single-row query asynchronously using Task.
        //
        // Parameters:
        //   type:
        //     The type to return.
        //
        //   sql:
        //     The SQL to execute for the query.
        //
        //   param:
        //     The parameters to pass, if any.
        //
        //   transaction:
        //     The transaction to use, if any.
        //
        //   commandTimeout:
        //     The command timeout (in seconds).
        //
        //   commandType:
        //     The type of command to execute.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     type is null.
        Task<object> QuerySingleAsync(Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Execute a single-row query asynchronously using Task.
        //
        // Parameters:
        //   type:
        //     The type to return.
        //
        //   command:
        //     The command used to query on this connection.
        Task<object> QuerySingleAsync(Type type, CommandDefinition command);
        //
        // Summary:
        //     Execute a single-row query asynchronously using Task.
        //
        // Parameters:
        //   command:
        //     The command used to query on this connection.
        //
        // Type parameters:
        //   T:
        //     The type to return.
        Task<T> QuerySingleAsync<T>(CommandDefinition command);
        //
        // Summary:
        //     Execute a single-row query asynchronously using Task.
        //
        // Parameters:
        //   command:
        //     The command used to query on this connection.
        //
        // Remarks:
        //     Note: the row can be accessed via "dynamic", or by casting to an IDictionary<string,object>
        Task<dynamic> QuerySingleAsync(CommandDefinition command);
        //
        // Summary:
        //     Executes a query, returning the data typed as T.
        //
        // Parameters:
        //   command:
        //     The command used to query on this connection.
        //
        // Type parameters:
        //   T:
        //     The type of results to return.
        //
        // Returns:
        //     A single instance of the supplied type; if a basic type (int, string, etc) is
        //     queried then the data from the first column is assumed, otherwise an instance
        //     is created per row, and a direct column-name===member-name mapping is assumed
        //     (case insensitive).
        T QuerySingleOrDefault<T>(CommandDefinition command);
        //
        // Summary:
        //     Return a dynamic object with properties matching the columns.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for the query.
        //
        //   param:
        //     The parameters to pass, if any.
        //
        //   transaction:
        //     The transaction to use, if any.
        //
        //   commandTimeout:
        //     The command timeout (in seconds).
        //
        //   commandType:
        //     The type of command to execute.
        //
        // Remarks:
        //     Note: the row can be accessed via "dynamic", or by casting to an IDictionary<string,object>
        dynamic QuerySingleOrDefault(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Executes a single-row query, returning the data typed as T.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for the query.
        //
        //   param:
        //     The parameters to pass, if any.
        //
        //   transaction:
        //     The transaction to use, if any.
        //
        //   commandTimeout:
        //     The command timeout (in seconds).
        //
        //   commandType:
        //     The type of command to execute.
        //
        // Type parameters:
        //   T:
        //     The type of result to return.
        //
        // Returns:
        //     A sequence of data of the supplied type; if a basic type (int, string, etc) is
        //     queried then the data from the first column is assumed, otherwise an instance
        //     is created per row, and a direct column-name===member-name mapping is assumed
        //     (case insensitive).
        T QuerySingleOrDefault<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Executes a single-row query, returning the data typed as type.
        //
        // Parameters:
        //   type:
        //     The type to return.
        //
        //   sql:
        //     The SQL to execute for the query.
        //
        //   param:
        //     The parameters to pass, if any.
        //
        //   transaction:
        //     The transaction to use, if any.
        //
        //   commandTimeout:
        //     The command timeout (in seconds).
        //
        //   commandType:
        //     The type of command to execute.
        //
        // Returns:
        //     A sequence of data of the supplied type; if a basic type (int, string, etc) is
        //     queried then the data from the first column is assumed, otherwise an instance
        //     is created per row, and a direct column-name===member-name mapping is assumed
        //     (case insensitive).
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     type is null.
        object QuerySingleOrDefault(Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Execute a single-row query asynchronously using Task.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for the query.
        //
        //   param:
        //     The parameters to pass, if any.
        //
        //   transaction:
        //     The transaction to use, if any.
        //
        //   commandTimeout:
        //     The command timeout (in seconds).
        //
        //   commandType:
        //     The type of command to execute.
        Task<dynamic> QuerySingleOrDefaultAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Execute a single-row query asynchronously using Task.
        //
        // Parameters:
        //   type:
        //     The type to return.
        //
        //   sql:
        //     The SQL to execute for the query.
        //
        //   param:
        //     The parameters to pass, if any.
        //
        //   transaction:
        //     The transaction to use, if any.
        //
        //   commandTimeout:
        //     The command timeout (in seconds).
        //
        //   commandType:
        //     The type of command to execute.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     type is null.
        Task<object> QuerySingleOrDefaultAsync(Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //
        // Summary:
        //     Execute a single-row query asynchronously using Task.
        //
        // Parameters:
        //   type:
        //     The type to return.
        //
        //   command:
        //     The command used to query on this connection.
        Task<object> QuerySingleOrDefaultAsync(Type type, CommandDefinition command);
        //
        // Summary:
        //     Execute a single-row query asynchronously using Task.
        //
        // Parameters:
        //   command:
        //     The command used to query on this connection.
        //
        // Type parameters:
        //   T:
        //     The type to return.
        Task<T> QuerySingleOrDefaultAsync<T>(CommandDefinition command);
        //
        // Summary:
        //     Execute a single-row query asynchronously using Task.
        //
        // Parameters:
        //   command:
        //     The command used to query on this connection.
        //
        // Remarks:
        //     Note: the row can be accessed via "dynamic", or by casting to an IDictionary<string,object>
        Task<dynamic> QuerySingleOrDefaultAsync(CommandDefinition command);
        //
        // Summary:
        //     Execute a single-row query asynchronously using Task.
        //
        // Parameters:
        //   sql:
        //     The SQL to execute for the query.
        //
        //   param:
        //     The parameters to pass, if any.
        //
        //   transaction:
        //     The transaction to use, if any.
        //
        //   commandTimeout:
        //     The command timeout (in seconds).
        //
        //   commandType:
        //     The type of command to execute.
        //
        // Type parameters:
        //   T:
        //     The type to return.
        Task<T> QuerySingleOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
    }
}

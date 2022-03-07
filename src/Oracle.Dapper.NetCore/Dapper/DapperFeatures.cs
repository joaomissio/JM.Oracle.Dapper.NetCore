using Dapper;
using System;
using System.Collections.Generic;
using System.Data;

namespace Oracle.Dapper.NetCore.Dapper
{
    public class DapperFeatures : IDapperFeatures
    {
        private readonly IDbConnection _connection;
        public DapperFeatures(IDbConnection connection)
        {
            _connection = connection;
        }

        public int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) =>
            _connection.Execute(sql, param, transaction, commandTimeout, commandType);

        public int Execute(CommandDefinition command) =>
            _connection.Execute(command);

        public IDataReader ExecuteReader(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) =>
            _connection.ExecuteReader(sql, param, transaction, commandTimeout, commandType);

        public IDataReader ExecuteReader(CommandDefinition command) =>
            _connection.ExecuteReader(command);

        public IDataReader ExecuteReader(CommandDefinition command, CommandBehavior commandBehavior) =>
            _connection.ExecuteReader(command, commandBehavior);

        public object ExecuteScalar(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) =>
            _connection.ExecuteScalar(sql, param, transaction, commandTimeout, commandType);

        public T ExecuteScalar<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) =>
            _connection.ExecuteScalar<T>(sql, param, transaction, commandTimeout, commandType);

        public object ExecuteScalar(CommandDefinition command) =>
            _connection.ExecuteScalar(command);

        public T ExecuteScalar<T>(CommandDefinition command) =>
            _connection.ExecuteScalar<T>(command);

        public dynamic QueryFirstOrDefault(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) =>
            _connection.QueryFirstOrDefault(sql, param, transaction, commandTimeout, commandType);

        public T QueryFirstOrDefault<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) =>
            _connection.QueryFirstOrDefault(sql, param, transaction, commandTimeout, commandType);

        public object QueryFirstOrDefault(Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) =>
            _connection.QueryFirstOrDefault(type, sql, param, transaction, commandTimeout, commandType);

        public T QueryFirstOrDefault<T>(CommandDefinition command) =>
            _connection.QueryFirstOrDefault<T>(command);

        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null) =>
            _connection.Query(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);

        public IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null) =>
            _connection.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);

        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null) =>
            _connection.Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);

        public IEnumerable<dynamic> Query(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null) =>
            _connection.Query(sql, param, transaction, buffered, commandTimeout, commandType);

        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null) =>
            _connection.Query<TFirst, TSecond, TThird, TFourth, TReturn>(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);

        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null) =>
             _connection.Query<TFirst, TSecond, TThird, TReturn>(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);

        public IEnumerable<T> Query<T>(CommandDefinition command) =>
            _connection.Query<T>(command);

        public IEnumerable<object> Query(Type type, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null) =>
            _connection.Query(type, sql, param, transaction, buffered, commandTimeout, commandType);

        public IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null) =>
            _connection.Query<TFirst, TSecond, TReturn>(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);

        public IEnumerable<TReturn> Query<TReturn>(string sql, Type[] types, Func<object[], TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null) =>
            _connection.Query<TReturn>(sql, types, map, param, transaction, buffered, splitOn, commandTimeout, commandType);

        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null) =>
            _connection.Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
    }
}

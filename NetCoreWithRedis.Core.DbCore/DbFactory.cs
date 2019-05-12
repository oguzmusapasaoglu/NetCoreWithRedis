using Dapper;
using NetCoreWithRedis.Core.Helper.CommonHelper;
using NetCoreWithRedis.Core.Helper.ExceptionHelper;
using NetCoreWithRedis.Core.Log.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace NetCoreWithRedis.Core.DbCore
{
    public class DbFactory : IDbFactory
    {
        private ILogService _logger;

        public IDbTransaction _DbTransaction { get; set; }
        public DbFactory(ILogService logger)
        {
            _logger = logger;
        }
        public DbTransaction _Transaction { get; set; }
        public DbConnection CreateConnection(bool parmNeedTrans = true)
        {
            try
            {
                var connection = new SqlConnection(ConfigManager.GetData(ConfigKey.ConnStr));
                if (connection.State == ConnectionState.Open)
                    connection.Close();

                connection.Open();
                if (parmNeedTrans)
                    _Transaction = connection.BeginTransaction();
                return connection;
            }
            catch (SqlException ex)
            {
                throw new KnownException(ErrorTypeEnum.DbOperationException, ex.Message, ex);
            }
        }
        public void CloseConnection(DbConnection conn)
        {
            try
            {
                conn.Close();
                conn.Dispose();
            }
            catch (SqlException ex)
            {
                throw new KnownException(ErrorTypeEnum.DbOperationException, ex.Message, ex);
            }
        }
        public TResult InsertOrUpdate<TResult>(GeneratedQuery InsertQuerry, object data)
        {
            try
            {
                var connection = CreateConnection();
                connection.Execute(InsertQuerry.QueryScript.ToString(), data, _DbTransaction);
                var Id = (int)connection.Query(@"select @@IDENTITY as ID").First().ID;
                var sb = new StringBuilder();
                sb.AppendFormat("SELECT * FROM {0} WHERE Id = {1}", InsertQuerry.TableName, Id);
                var Result = connection.QueryFirst<TResult>(sb.ToString());
                _Transaction.Commit();
                CloseConnection(connection);
                return Result;
            }
            catch (SqlException ex)
            {
                _logger.AddFattalLog(LogTypeEnum.Error, "DbFactory.InsertOrUpdate", ExceptionMessageHelper.UnexpectedDBError, InsertQuerry.QueryScript, ex);
                throw new KnownException(ErrorTypeEnum.DbOperationException, ex.Message, ex);
            }
            catch (Exception e)
            {
                _logger.AddFattalLog(LogTypeEnum.Error, "DbFactory.InsertOrUpdate", ExceptionMessageHelper.UnexpectedDBError, InsertQuerry.QueryScript, e);
                throw new KnownException(ErrorTypeEnum.UnexpectedExeption, e.Message, e);
            }
        }
        public TResult InsertOrUpdate<TResult>(GeneratedQuery InsertQuerry)
        {
            try
            {
                var connection = CreateConnection();
                connection.ExecuteScalar(InsertQuerry.QueryScript.ToString(), InsertQuerry.Parameters, _Transaction);
                var Id = (int)connection.Query(@"select @@IDENTITY as ID").First().ID;
                var sb = new StringBuilder();
                sb.AppendFormat("SELECT * FROM {0} WHERE Id = {1}", InsertQuerry.TableName, Id);
                var Result = connection.QueryFirst<TResult>(sb.ToString());
                _Transaction.Commit();
                CloseConnection(connection);
                return Result;
            }
            catch (SqlException ex)
            {
                _logger.AddFattalLog(LogTypeEnum.Error, "DbFactory.InsertOrUpdate", ExceptionMessageHelper.UnexpectedDBError, InsertQuerry.QueryScript, ex);
                throw new KnownException(ErrorTypeEnum.DbOperationException, ex.Message, ex);
            }
            catch (Exception e)
            {
                _logger.AddFattalLog(LogTypeEnum.Error, "DbFactory.InsertOrUpdate", ExceptionMessageHelper.UnexpectedDBError, InsertQuerry.QueryScript, e);
                throw new KnownException(ErrorTypeEnum.UnexpectedExeption, e.Message, e);
            }
        }
        public TResult InsertOrUpdateWithSp<TResult>(string SpName, object parm)
        {
            try
            {
                var connection = CreateConnection();
                var result = connection.ExecuteScalar<TResult>(SpName, parm, commandType: CommandType.StoredProcedure);
                CloseConnection(connection);
                return result;
            }
            catch (SqlException ex)
            {
                _logger.AddFattalLog(LogTypeEnum.Fattal, "DbFactory.InsertOrUpdateWithSp", ExceptionMessageHelper.UnexpectedDBError, SpName, ex);

                _Transaction.Rollback();
                throw new KnownException(ErrorTypeEnum.DbOperationException, ex.Message, ex);
            }
            catch (Exception e)
            {
                _logger.AddFattalLog(LogTypeEnum.Fattal, "DbFactory.InsertOrUpdateWithSp", ExceptionMessageHelper.UnexpectedDBError, SpName, e);

                _Transaction.Rollback();
                throw new KnownException(ErrorTypeEnum.UnexpectedExeption, e.Message, e);
            }
        }
        public IEnumerable<TResult> GetData<TResult>(GeneratedQuery query)
        {
            try
            {
                var connection = CreateConnection(false);
                var result = connection.Query<TResult>(query.QueryScript.ToString(), query.Parameters);
                CloseConnection(connection);
                return result;
            }
            catch (SqlException ex)
            {
                _logger.AddFattalLog(LogTypeEnum.Fattal, "DbFactory.GetData", ExceptionMessageHelper.UnexpectedDBError, query.QueryScript, ex);

                _Transaction.Rollback();
                throw new KnownException(ErrorTypeEnum.DbOperationException, ex.Message, ex);
            }
            catch (Exception e)
            {
                _logger.AddFattalLog(LogTypeEnum.Fattal, "DbFactory.GetData", ExceptionMessageHelper.UnexpectedDBError, query.QueryScript, e);

                _Transaction.Rollback();
                throw new KnownException(ErrorTypeEnum.UnexpectedExeption, e.Message, e);
            }
        }
        public IEnumerable<TResult> GetData<TResult>(GeneratedQuery query, object parm)
        {
            try
            {
                var connection = CreateConnection(false);
                var result = connection.Query<TResult>(query.QueryScript.ToString(), parm);
                CloseConnection(connection);
                return result;
            }
            catch (SqlException ex)
            {
                _logger.AddFattalLog(LogTypeEnum.Fattal, "DbFactory.GetData", ExceptionMessageHelper.UnexpectedDBError, query.QueryScript, ex);

                _Transaction.Rollback();
                throw new KnownException(ErrorTypeEnum.DbOperationException, ex.Message, ex);
            }
            catch (Exception e)
            {
                _logger.AddFattalLog(LogTypeEnum.Fattal, "DbFactory.GetData", ExceptionMessageHelper.UnexpectedDBError, query.QueryScript, e);

                _Transaction.Rollback();
                throw new KnownException(ErrorTypeEnum.UnexpectedExeption, e.Message, e);
            }
        }
        public IEnumerable<TResult> GetDataWithSp<TResult>(string SpName, object parm)
        {
            try
            {
                var connection = CreateConnection(false);
                var result = connection.Query<TResult>(SpName, parm, commandType: CommandType.StoredProcedure);
                CloseConnection(connection);
                return result;
            }
            catch (SqlException ex)
            {
                _logger.AddFattalLog(LogTypeEnum.Fattal, "DbFactory.GetDataWithSp", ExceptionMessageHelper.UnexpectedDBError, SpName, ex);

                _Transaction.Rollback();
                throw new KnownException(ErrorTypeEnum.DbOperationException, ex.Message, ex);
            }
            catch (Exception e)
            {
                _logger.AddFattalLog(LogTypeEnum.Fattal, "DbFactory.GetDataWithSp", ExceptionMessageHelper.UnexpectedDBError, SpName, e);

                _Transaction.Rollback();
                throw new KnownException(ErrorTypeEnum.UnexpectedExeption, e.Message, e);
            }
        }
        public TResult GetSingleData<TResult>(GeneratedQuery query)
        {
            try
            {
                var connection = CreateConnection(false);
                var result = connection.QueryFirst<TResult>(query.QueryScript.ToString(), query.Parameters, null, 1000, CommandType.Text);
                CloseConnection(connection);
                return result;
            }
            catch (SqlException ex)
            {
                _logger.AddFattalLog(LogTypeEnum.Fattal, "DbFactory.GetSingleData", ExceptionMessageHelper.UnexpectedDBError, query.QueryScript, ex);

                _Transaction.Rollback();
                throw new KnownException(ErrorTypeEnum.DbOperationException, ex.Message, ex);
            }
            catch (Exception e)
            {
                _logger.AddFattalLog(LogTypeEnum.Fattal, "DbFactory.GetSingleData", ExceptionMessageHelper.UnexpectedDBError, query.QueryScript, e);

                _Transaction.Rollback();
                throw new KnownException(ErrorTypeEnum.UnexpectedExeption, e.Message, e);
            }
        }
        public TResult GetSingleDataWithSp<TResult>(string SpName, object parm)
        {
            try
            {
                var connection = CreateConnection(false);
                var result = connection.QueryFirst<TResult>(SpName, parm, commandType: CommandType.StoredProcedure);
                CloseConnection(connection);
                return result;
            }
            catch (SqlException ex)
            {
                _logger.AddFattalLog(LogTypeEnum.Fattal, "DbFactory.GetSingleDataWithSp", ExceptionMessageHelper.UnexpectedDBError, SpName, ex);

                _Transaction.Rollback();
                throw new KnownException(ErrorTypeEnum.DbOperationException, ex.Message, ex);
            }
            catch (Exception e)
            {
                _logger.AddFattalLog(LogTypeEnum.Fattal, "DbFactory.GetSingleDataWithSp", ExceptionMessageHelper.UnexpectedDBError, SpName, e);

                _Transaction.Rollback();
                throw new KnownException(ErrorTypeEnum.UnexpectedExeption, e.Message, e);
            }
        }
        public TResult GetSingleDataWithId<TResult>(StringBuilder selectClause, int parmId, DbConnection dbConnection)
        {
            try
            {
                var connection = (dbConnection == null) ? CreateConnection(false) : dbConnection;
                var result = connection.QuerySingle<TResult>(selectClause.ToString(), new { Id = parmId });
                if (dbConnection == null)
                    CloseConnection(connection);
                return result;
            }
            catch (SqlException ex)
            {
                _logger.AddFattalLog(LogTypeEnum.Fattal, "DbFactory.GetSingleDataWithId", ExceptionMessageHelper.UnexpectedDBError, selectClause, ex);

                _Transaction.Rollback();
                throw new KnownException(ErrorTypeEnum.DbOperationException, ex.Message, ex);
            }
            catch (Exception e)
            {
                _logger.AddFattalLog(LogTypeEnum.Fattal, "DbFactory.GetSingleDataWithId", ExceptionMessageHelper.UnexpectedDBError, selectClause, e);

                _Transaction.Rollback();
                throw new KnownException(ErrorTypeEnum.UnexpectedExeption, e.Message, e);
            }
        }
        public IEnumerable<TResponse> GetDataPaging<TResponse>(int parmPageNumber, int parmRowPerPage, GeneratedQuery parmGeneratedPagingQuery)
        {
            var response = GetData<TResponse>(parmGeneratedPagingQuery);
            return response;
        }
    }
}

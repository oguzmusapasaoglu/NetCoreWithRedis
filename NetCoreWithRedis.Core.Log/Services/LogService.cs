using Dapper;
using Dapper.Contrib.Extensions;
using NetCoreWithRedis.Core.Log.Helper;
using NetCoreWithRedis.Core.Log.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace NetCoreWithRedis.Core.Log.Services
{
    public class LogService : ILogService
    {
        private readonly string LogConnStr = @"Data Source=OLORINSCOMPUTER\SQLEXPRESS;Initial Catalog=DominosLog;Integrated Security=True";

        public void AddLog(
            string parmUniqRequestId,
            LogTypeEnum parmLogType,
            string parmLogSenderFunc,
            int? parmLogUser = null,
            string parmLogMessage = null,
            string parmLogData = null,
            Exception parmException = null)
        {
            var dto = new LogEntity
            {
                UniqRequestId = parmUniqRequestId,
                LogUser = parmLogUser,
                LogType = (int)parmLogType,
                LogData = parmLogData,
                LogDate = DateTimeHelper.Now,
                LogMessage = parmLogMessage,
                LogSender = parmLogSenderFunc,
                LogExceptionMessage = (parmException != null) ? parmException.StackTrace : string.Empty
            };
            InsertLog(dto);
        }
        public void AddResponseLog(
            string parmUniqRequestId,
            LogTypeEnum parmLogType,
            string parmLogSenderFunc,
            int? parmLogUser,
            string parmLogMessage,
            string parmRequestData,
            long parmRequestDate,
            string parmResponseData,
            long parmResponseDate,
            Exception parmException = null
         )
        {
            var dto = new LogEntity
            {
                UniqRequestId = parmUniqRequestId,
                LogUser = parmLogUser,
                LogType = (int)parmLogType,
                RequestData = parmRequestData,
                RequestDate = parmRequestDate,
                ResponseData = parmResponseData,
                ResponseDate = parmResponseDate,
                LogDate = DateTimeHelper.Now,
                LogMessage = parmLogMessage,
                LogSender = parmLogSenderFunc,
                LogExceptionMessage = (parmException != null) ? parmException.StackTrace : string.Empty
            };
            InsertLog(dto);
        }
        public void AddLog(
            string parmUniqRequestId,
            LogTypeEnum parmLogType,
            string parmLogSenderFunc,
            int? parmLogUser = null,
            string parmLogMessage = null,
            StringBuilder parmLogData = null,
            Exception parmException = null
            )
        {
            var dto = new LogEntity
            {
                UniqRequestId = parmUniqRequestId,
                LogUser = parmLogUser,
                LogType = (int)parmLogType,
                LogData = parmLogData.ToString(),
                LogDate = DateTimeHelper.Now,
                LogMessage = parmLogMessage,
                LogSender = parmLogSenderFunc,
                LogExceptionMessage = (parmException != null) ? parmException.StackTrace : string.Empty
            };
            InsertLog(dto);
        }
        public void AddCacheLog(LogTypeEnum parmLogType, string parmLogSenderFunc, string parmLogMessage, Exception parmException)
        {
            var dto = new LogEntity
            {
                UniqRequestId = string.Empty,
                LogType = (int)parmLogType,
                LogDate = DateTimeHelper.Now,
                LogMessage = parmLogMessage,
                LogSender = parmLogSenderFunc,
                LogExceptionMessage = (parmException != null) ? parmException.StackTrace : string.Empty
            };
            InsertLog(dto);
        }
        public void AddFattalLog(LogTypeEnum parmLogType, string parmLogSenderFunc, string parmLogMessage, StringBuilder parmLogData, Exception parmException)
        {
            var dto = new LogEntity
            {
                UniqRequestId = string.Empty,
                LogType = (int)parmLogType,
                LogData = parmLogData.ToString(),
                LogDate = DateTimeHelper.Now,
                LogMessage = parmLogMessage,
                LogSender = parmLogSenderFunc,
                LogExceptionMessage = (parmException != null) ? parmException.StackTrace : string.Empty
            };
            InsertLog(dto);
        }
        public void AddFattalLog(LogTypeEnum parmLogType, string parmLogSenderFunc, string parmLogMessage, string parmLogData, Exception parmException)
        {
            var dto = new LogEntity
            {
                UniqRequestId = string.Empty,
                LogType = (int)parmLogType,
                LogData = parmLogData,
                LogDate = DateTimeHelper.Now,
                LogMessage = parmLogMessage,
                LogSender = parmLogSenderFunc,
                LogExceptionMessage = (parmException != null) ? parmException.StackTrace : string.Empty
            };
            InsertLog(dto);
        }
        public IEnumerable<LogEntity> GetLog(RequestLogEntity data)
        {
            string SpName = "";
            var conn = CreateConnection();
            try
            {
                var p = new
                {
                    data.LogUser,
                    data.LogType,
                    data.LogDateStart,
                    data.LogDateEnd
                };
                return conn.Query<LogEntity>(SpName, p);
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        #region Manager
        private SqlConnection CreateConnection()
        {
            try
            {
                var connection = new SqlConnection(LogConnStr);
                if (connection.State == ConnectionState.Open)
                    connection.Close();

                connection.Open();
                return connection;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        private void InsertLog(LogEntity data)
        {
            var conn = CreateConnection();
            try
            {
                conn.InsertAsync<LogEntity>(data);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
        #endregion
    }
}

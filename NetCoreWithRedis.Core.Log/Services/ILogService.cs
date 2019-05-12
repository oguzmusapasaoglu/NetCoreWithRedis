using NetCoreWithRedis.Core.Log.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreWithRedis.Core.Log.Interface
{
    public interface ILogService
    {
        void AddResponseLog(string parmUniqRequestId,
            LogTypeEnum parmLogType,
            string parmLogSenderFunc,
            int? parmLogUser,
            string parmLogMessage,
            string parmRequestData,
            long parmRequestDate,
            string parmResponseData,
            long parmResponseDate,
            Exception parmException = null
         );
        void AddLog(string UniqRequestId, LogTypeEnum parmLogType, string parmLogSenderFunc, int? parmLogUser = null, string parmLogMessage = null, string parmLogData = null, Exception parmException = null);
        void AddLog(string UniqRequestId, LogTypeEnum parmLogType, string parmLogSenderFunc, int? parmLogUser = null, string parmLogMessage = null, StringBuilder parmLogData = null, Exception parmException = null);
        IEnumerable<LogEntity> GetLog(RequestLogEntity data);
        void AddCacheLog(LogTypeEnum parmLogType, string parmLogSenderFunc, string parmLogMessage, Exception parmException);
        void AddFattalLog(LogTypeEnum parmLogType, string parmLogSenderFunc, string parmLogMessage,  StringBuilder parmLogData, Exception ex);
        void AddFattalLog(LogTypeEnum parmLogType, string parmLogSenderFunc, string parmLogMessage, string parmLogData, Exception ex);
    }
}

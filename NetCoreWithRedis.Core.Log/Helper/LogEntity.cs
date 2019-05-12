using Dapper.Contrib.Extensions;

namespace NetCoreWithRedis.Core.Log.Helper
{
    public class LogEntity
    {
        [Key]
        public int LogId { get; set; }
        public string UniqRequestId { get; set; }
        public int? LogUser { get; set; }
        public int LogType { get; set; }
        public long LogDate { get; set; }
        public string LogSender { get; set; }
        public string LogMessage { get; set; }
        public string LogData { get; set; }
        public string LogExceptionMessage { get; set; }
        public string RequestData { get; set; }
        public long? RequestDate { get; set; }
        public string ResponseData { get; set; }
        public long? ResponseDate { get; set; }
    }

    public class RequestLogEntity
    {
        public int? LogUser { get; set; }
        public int? LogType { get; set; }
        public long? LogDateStart { get; set; }
        public long? LogDateEnd { get; set; }
    }
}

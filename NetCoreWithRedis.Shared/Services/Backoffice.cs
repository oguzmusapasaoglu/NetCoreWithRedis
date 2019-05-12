using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NetCoreWithRedis.Shared.Services
{
    [DataContract]
    public class BackofficeRequest
    {
        [DataMember(Name = "EventName")]
        public string EventName { get; set; }

        [DataMember(Name = "RData")]
        public string RData { get; set; }
    }

    [Serializable]
    [DataContract]
    public class BackofficeRequestData<T>
    {
        public BackofficeRequestData(string parmUniqRequestId, string parmEventName, int parmRUserId, string parmLang, string parmTokenKey, int parmAppMapId, T parmRData)
        {
            UniqRequestId = parmUniqRequestId;
            EventName = parmEventName;
            RUserId = parmRUserId;
            Lang = parmLang;
            TokenKey = parmTokenKey;
            AppMapId = parmAppMapId;
            RData = parmRData;
        }

        [DataMember(Name = "UniqRequestId")]
        public string UniqRequestId { get; set; }

        [DataMember(Name = "EventName")]
        public string EventName { get; set; }

        [DataMember(Name = "RUserId")]
        public int RUserId { get; set; }

        [DataMember(Name = "Lang")]
        public string Lang { get; set; }

        [DataMember(Name = "TokenKey")]
        public string TokenKey { get; set; }

        [DataMember(Name = "AppMapId")]
        public int AppMapId { get; set; }

        [DataMember(Name = "RData")]
        public T RData { get; set; }
    }

    [DataContract]
    public class BackofficeResponse
    {
        [DataMember(Name = "data")]
        public string data { get; set; }
    }

    [DataContract]
    public class BackofficeResponseData<T>
    {
        public BackofficeResponseData()
        {
            ErrorMessageList = ErrorMessageList ?? new List<string>();
        }
        [DataMember(Name = "Data")]
        public T Data { get; set; }

        [DataMember(Name = "ResponseStatus")]
        public bool ResponseStatus { get; set; }

        [DataMember(Name = "ErrorType")]
        public string ErrorType { get; set; }

        [DataMember(Name = "ErrorMessageList")]
        public List<string> ErrorMessageList { get; set; }
    }
}

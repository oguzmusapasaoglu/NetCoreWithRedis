using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NetCoreWithRedis.Shared.Services
{

    [DataContract]
    public class GlobalRequest
    {
        [DataMember(Name = "EventName")]
        public string EventName { get; set; }

        [DataMember(Name = "RData")]
        public string RData { get; set; }
    }

    [Serializable]
    [DataContract]
    public class GlobalRequestData<T>
    {
        [DataMember(Name = "UniqRequestId")]
        public string UniqRequestId { get; set; }

        [DataMember(Name = "EventName")]
        public string EventName { get; set; }

        [DataMember(Name = "Lang")]
        public string Lang { get; set; }

        [DataMember(Name = "RData")]
        public T RData { get; set; }
    }

    [DataContract]
    public class GlobalResponse
    {
        [DataMember(Name = "data")]
        public string data { get; set; }
    }

    [DataContract]
    public class GlobalResponseData<T>
    {
        public GlobalResponseData()
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

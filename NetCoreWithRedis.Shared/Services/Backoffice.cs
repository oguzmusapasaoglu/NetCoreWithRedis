using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NetCoreWithRedis.Shared.Services
{
    [Serializable]
    [DataContract]
    public class BackofficeRequest
    {
        public BackofficeRequest(string parmEventName, int parmRUserId, string parmLang, string parmTokenKey, int parmAppMapId, string parmRData)
        {
            RUserId = parmRUserId;
            TokenKey = parmTokenKey;
            RData = parmRData;
        }

        [DataMember(Name = "RUserId")]
        public int RUserId { get; set; }

        [DataMember(Name = "TokenKey")]
        public string TokenKey { get; set; }

        [DataMember(Name = "RData")]
        public string RData { get; set; }
    }

    [DataContract]
    public class BackofficeResponse
    {
        [DataMember(Name = "Data")]
        public string Data { get; set; }

        [DataMember(Name = "ResponseStatus")]
        public bool ResponseStatus { get; set; }

        [DataMember(Name = "ErrorType")]
        public string ErrorType { get; set; }

        [DataMember(Name = "ErrorMessage")]
        public string ErrorMessage { get; set; }
    }
}

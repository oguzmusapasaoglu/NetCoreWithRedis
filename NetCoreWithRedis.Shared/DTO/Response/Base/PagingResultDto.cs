using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NetCoreWithRedis.Shared.DTO.Response.Base
{
    [System.Serializable]
    public class PagingResultDto<TResponse>
    {
        [DataMember(Name = "Response")]
        public TResponse Response { get; set; }

        [DataMember(Name = "CurrPage")]
        public int CurrPage { get; set; }

        [DataMember(Name = "TotalRowCount")]
        public int TotalRowCount { get; set; }

        [DataMember(Name = "TotalPage")]
        public int TotalPage { get; set; }
    }
}

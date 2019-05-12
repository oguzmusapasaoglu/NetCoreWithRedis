using System.Runtime.Serialization;

namespace NetCoreWithRedis.Shared.DTO.Response.Base
{
    [DataContract]
    public class PagingResult<TResponse>
    {
        [DataMember(Name = "Response")]
        public TResponse Response { get; set; }

        [DataMember(Name = "CurrPage")]
        public int CurrPage { get; set; }

        [DataMember(Name = "TotalPage")]
        public int TotalPage { get; set; }
    }
}

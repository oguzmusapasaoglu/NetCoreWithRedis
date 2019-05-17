using System.Runtime.Serialization;

namespace NetCoreWithRedis.Shared.DTO.Response.Base
{
    [DataContract]
    public class BaseFilterResponseDto
    {
        [IgnoreDataMember]
        [DataMember(Name = "TotalRowCount")]
        public int TotalRowCount { get; set; }
    }
}

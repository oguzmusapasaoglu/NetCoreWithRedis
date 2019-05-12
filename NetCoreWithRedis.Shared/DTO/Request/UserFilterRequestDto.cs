using NetCoreWithRedis.Shared.Base.DTO;
using System.Runtime.Serialization;

namespace NetCoreWithRedis.Shared.DTO.Request
{
    [DataContract]
    public class UserFilterRequestDto : BaseFilterDto
    {
        [DataMember(Name = "Id")]
        public int? Id { get; set; }

        [DataMember(Name = "ActivationStatus")]
        public int? ActivationStatus { get; set; }

        [DataMember(Name = "UserName")]
        public string UserName { get; set; }

        [DataMember(Name = "FullName")]
        public string FullName { get; set; }

        [DataMember(Name = "EMail")]
        public string EMail { get; set; }

        [DataMember(Name = "AuthorizationGroupId")]
        public int? AuthorizationGroupId { get; set; }
    }
}

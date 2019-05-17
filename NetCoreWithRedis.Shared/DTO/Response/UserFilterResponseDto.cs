using NetCoreWithRedis.Shared.DTO.Response.Base;
using System.Runtime.Serialization;

namespace NetCoreWithRedis.Shared.DTO.Response
{
    [DataContract]
    public class UserFilterResponseDto : BaseFilterResponseDto
    {
        [DataMember(Name = "Id")]
        public int? Id { get; set; }

        [DataMember(Name = "ActivationStatusProp")]
        public int ActivationStatusProp { get; set; }

        [DataMember(Name = "ActivationStatus")]
        public string ActivationStatus { get; set; }

        [DataMember(Name = "UserName")]
        public string UserName { get; set; }

        [DataMember(Name = "Password")]
        public string Password { get; set; }

        [DataMember(Name = "FullName")]
        public string FullName { get; set; }

        [DataMember(Name = "Email")]
        public string Email { get; set; }

        [DataMember(Name = "CreatedUserId")]
        public int CreatedUserId { get; set; }

        [DataMember(Name = "CreationDate")]
        public long CreationDate { get; set; }

        [DataMember(Name = "UpdatedUserId")]
        public int? UpdatedUserId { get; set; }

        [DataMember(Name = "UpdateDate")]
        public long? UpdateDate { get; set; }
    }
}

using System.Runtime.Serialization;

namespace NetCoreWithRedis.Shared.DTO.Request
{
    [DataContract]
    public class UserPasswordRequestDto
    {
        [DataMember(Name = "UserId")]
        public int UserId { get; set; }

        [DataMember(Name = "Email")]
        public string Email { get; set; }

        [DataMember(Name = "OldPass")]
        public string OldPass { get; set; }

        [DataMember(Name = "NewPass")]
        public string NewPass { get; set; }
    }
}

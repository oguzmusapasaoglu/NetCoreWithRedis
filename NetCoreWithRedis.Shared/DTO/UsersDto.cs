using System.Runtime.Serialization;

namespace NetCoreWithRedis.Shared.DTO
{
    public class UsersDto : ExtendBaseDto
    {
        [DataMember(Name = "UserName")]
        public string UserName { get; set; }

        [DataMember(Name = "Password")]
        public string Password { get; set; }

        [DataMember(Name = "FullName")]
        public string FullName { get; set; }

        [DataMember(Name = "EMail")]
        public string EMail { get; set; }
    }
}

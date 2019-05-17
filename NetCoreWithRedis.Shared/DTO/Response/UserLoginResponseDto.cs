using System.Runtime.Serialization;

namespace NetCoreWithRedis.Shared.DTO.Response
{
    public class UserLoginResponseDto : UsersDto
    {
        [ServiceStack.DataAnnotations.Ignore]
        [DataMember(Name = "TokenKey")]
        public string TokenKey { get; set; }
    }
}

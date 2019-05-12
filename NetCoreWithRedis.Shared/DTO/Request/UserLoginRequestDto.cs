﻿using System.Runtime.Serialization;

namespace NetCoreWithRedis.Shared.DTO.Request
{
    [DataContract]
    public class UserLoginRequestDto
    {
        [DataMember(Name = "UserName")]
        public string UserName { get; set; }

        [DataMember(Name = "Password")]
        public string Password { get; set; }
    }
}

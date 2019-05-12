using NetCoreWithRedis.Shared.DTO.Response.Base;
using System;
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
        public string ActivationStatus{ get; set; }

        [DataMember(Name = "AuthorizationGroupName")]
        public string AuthorizationGroupName { get; set; }
        
        [DataMember(Name = "AuthorizationGroupId")]
        public int AuthorizationGroupId { get; set; }

        [DataMember(Name = "UserName")]
        public string UserName { get; set; }

        [DataMember(Name = "Password")]
        public string Password { get; set; }

        [DataMember(Name = "FullName")]
        public string FullName { get; set; }

        [DataMember(Name = "Email")]
        public string Email { get; set; }

        [DataMember(Name = "BirthDate")]
        public DateTime? BirthDate { get; set; }

        [DataMember(Name = "ConfirmStatusProp")]
        public int ConfirmStatusProp { get; set; }

        [DataMember(Name = "ConfirmStatus")]
        public string ConfirmStatus { get; set; }

        [DataMember(Name = "FirstPhoneNumber")]
        public string FirstPhoneNumber { get; set; }

        [DataMember(Name = "SecondPhoneNumber")]
        public string SecondPhoneNumber { get; set; }

        [DataMember(Name = "FailedLoginCount")]
        public int FailedLoginCount { get; set; }

        [DataMember(Name = "CreatedUserId")]
        public int CreatedUserId { get; set; }

        [DataMember(Name = "CreationDate")]
        public long CreationDate { get; set; }

        [DataMember(Name = "UpdatedUserId")]
        public int? UpdatedUserId { get; set; }

        [DataMember(Name = "UpdateDate")]
        public long? UpdateDate { get; set; }

        [DataMember(Name = "UsersTypeProp")]
        public int UsersTypeProp { get; set; }

        [DataMember(Name = "UsersType")]
        public string UsersType { get; set; }
    }
}

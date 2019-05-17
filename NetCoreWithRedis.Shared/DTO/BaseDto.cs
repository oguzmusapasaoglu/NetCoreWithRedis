using System.Runtime.Serialization;

namespace NetCoreWithRedis.Shared.DTO
{
    public class BaseDto
    {
        [DataMember(Name = "Id")]
        public int Id { get; set; }

        [DataMember(Name = "ActivationStatus")]
        public int ActivationStatus { get; set; }
    }
    public class ExtendBaseDto : BaseDto
    {
        [DataMember(Name = "CreateDate")]
        public long CreateDate { get; set; }

        [DataMember(Name = "CreateUserId")]
        public int CreateUserId { get; set; }

        [DataMember(Name = "UpdateDate")]
        public long? UpdateDate { get; set; }

        [DataMember(Name = "UpdateUser")]
        public int? UpdateUser { get; set; }
    }
}

using NetCoreWithRedis.Shared.Base.DTO;

namespace NetCoreWithRedis.Shared.DTO.Request
{
    public class CustomerFilterRequestDto : BaseFilterDto
    {
        //[DataMember(Name = "Id")]
        public int? Id { get; set; }
        //[DataMember(Name = "ActivationStatus")]
        public int? ActivationStatus { get; set; }
        //[DataMember(Name = "UserName")]
        public string UserName { get; set; }
        //[DataMember(Name = "FullName")]
        public string FullName { get; set; }
    }
}

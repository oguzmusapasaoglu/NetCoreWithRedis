using System.Runtime.Serialization;

namespace NetCoreWithRedis.Shared.DTO
{
    public class ProductTypeDto : ExtendBaseDto
    {
        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "Description")]
        public string Description { get; set; }
    }
}

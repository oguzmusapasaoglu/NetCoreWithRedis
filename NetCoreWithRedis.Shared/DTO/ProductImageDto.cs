using System.Runtime.Serialization;

namespace NetCoreWithRedis.Shared.DTO
{
    public class ProductImageDto : ExtendBaseDto
    {
        [DataMember(Name = "ProductId")]
        public int ProductId { get; set; }

        [DataMember(Name = "ImageUrl")]
        public string ImageUrl { get; set; }
    }
}

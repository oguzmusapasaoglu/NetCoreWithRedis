using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NetCoreWithRedis.Shared.DTO
{
    public class ProductDto : ExtendBaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double? DiscountPrice { get; set; }
        public int ProductTypeId { get; set; }
        public ProductTypeDto ProductTypeProp { get; set; }
        public IEnumerable<ProductImageDto> ProductImagesProp { get; set; }
    }
}

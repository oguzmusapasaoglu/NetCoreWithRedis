using NetCoreWithRedis.Shared.Base.DTO;

namespace NetCoreWithRedis.Shared.DTO.Request
{
    [System.Serializable]
    public class ProductFilterRequestDto : BaseFilterDto
    {
        public string Name { get; set; }
        public double? PriceStart { get; set; }
        public double? PriceEnd { get; set; }
        public double? DiscountPriceStart { get; set; }
        public double? DiscountPriceEnd { get; set; }
        public int? ProductTypeId { get; set; }
    }
}

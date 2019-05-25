using Dapper.Contrib.Extensions;
using NetCoreWithRedis.Core.Helper.EntityHelper;

namespace NetCoreWithRedis.Domain.Product.Entity
{
    [Table("Product")]
    public class ProductEntity : ExtendBaseEntity
    {
        public string Name  { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double? DiscountPrice { get; set; }
        public int ProductTypeId { get; set; }
    }
}

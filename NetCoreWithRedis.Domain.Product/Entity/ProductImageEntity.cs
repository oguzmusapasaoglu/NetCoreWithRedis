using Dapper.Contrib.Extensions;
using NetCoreWithRedis.Core.Helper.EntityHelper;

namespace NetCoreWithRedis.Domain.Product.Entity
{
    [Table("ProductImage")]
    public class ProductImageEntity : ExtendBaseEntity
    {
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }
    }
}

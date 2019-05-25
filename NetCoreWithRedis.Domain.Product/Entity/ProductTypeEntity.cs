using Dapper.Contrib.Extensions;
using NetCoreWithRedis.Core.Helper.EntityHelper;

namespace NetCoreWithRedis.Domain.Product.Entity
{
    [Table("ProductType")]
    public class ProductTypeEntity : ExtendBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

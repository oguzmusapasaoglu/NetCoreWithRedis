using Dapper.Contrib.Extensions;
using NetCoreWithRedis.Core.Helper.EntityHelper;

namespace NetCoreWithRedis.Domain.Users.Entity
{
    [Table("Customer")]
    public class CustomerEntity : ExtendBaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
    }
}

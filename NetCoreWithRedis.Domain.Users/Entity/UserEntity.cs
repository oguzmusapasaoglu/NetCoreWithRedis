using Dapper.Contrib.Extensions;
using NetCoreWithRedis.Core.Helper.EntityHelper;

namespace NetCoreWithRedis.Domain.Users.Entity
{
    [Table("User")]
    public class UserEntity : ExtendBaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string EMail { get; set; }
    }
}

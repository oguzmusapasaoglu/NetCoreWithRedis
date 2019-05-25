using NetCoreWithRedis.Core.Helper.EntityHelper;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreWithRedis.Domain.Users.Entity
{
    [Table("CustomerAddress")]
    public class CustomerAdressEntitiy : ExtendBaseEntity
    {
        public int CustomerId { get; set; }
        public string AddressTitle { get; set; }
        public string AddressDescription { get; set; }
        public string AddressDirections { get; set; }
        public string FirstContactNumber { get; set; }
        public string SecondContactNumber { get; set; }
    }
}

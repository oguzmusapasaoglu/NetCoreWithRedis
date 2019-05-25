using System.Collections.Generic;

namespace NetCoreWithRedis.Shared.DTO
{
    public class CustomerDto : ExtendBaseDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public IEnumerable<CustomerAddressDto> CustomerAddressList { get; set; }
    }
}

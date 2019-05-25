using ServiceStack.DataAnnotations;

namespace NetCoreWithRedis.Shared.DTO
{
    public class CustomerAddressDto : ExtendBaseDto
    {
        [Required]
        public int CustomerId { get; set; }
        public string AddressTitle { get; set; }
        public string AddressDescription { get; set; }
        public string AddressDirections { get; set; }
        public string FirstContactNumber { get; set; }
        public string SecondContactNumber { get; set; }
    }
}

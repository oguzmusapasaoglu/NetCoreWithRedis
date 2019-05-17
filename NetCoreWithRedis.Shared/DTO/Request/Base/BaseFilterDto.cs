using ServiceStack.DataAnnotations;
using System.Runtime.Serialization;

namespace NetCoreWithRedis.Shared.Base.DTO
{
    public class BaseFilterDto
    {
        [Required]
        [DataMember(Name = "PageNumber")]
        public int PageNumber { get; set; }

        [Required]
        [DataMember(Name = "PageSize")]
        public int PageSize { get; set; }
    }
}

using ServiceStack.DataAnnotations;
using System.Runtime.Serialization;

namespace NetCoreWithRedis.Shared.Base.DTO
{
    public class BaseFilterDto
    {
        [Required]
        public int PageNumber { get; set; }

        [Required]
        public int PageSize { get; set; }
    }
}

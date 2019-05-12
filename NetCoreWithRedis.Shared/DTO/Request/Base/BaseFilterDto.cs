using ServiceStack.DataAnnotations;
using System.Runtime.Serialization;

namespace NetCoreWithRedis.Shared.Base.DTO
{
    [DataContract]
    public class BaseFilterDto
    {
        [Required]
        [DataMember(Name = "PageNumber")]
        public int PageNumber { get; set; }

        [Required]
        [DataMember(Name = "RowPerPage")]
        public int RowsPerPage { get; set; }
    }
}

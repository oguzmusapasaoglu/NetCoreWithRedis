namespace NetCoreWithRedis.Core.Helper.EntityHelper
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public int ActivationStatus { get; set; }
    }
    public class ExtendBaseEntity : BaseEntity
    {
        public long CreateDate   { get; set; }
        public int CreateUserId     { get; set; }
        public long? UpdateDate   { get; set; }
        public int? UpdateUser { get; set; }
    }
}

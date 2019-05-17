namespace NetCoreWithRedis.Domain.Authentication.Entity
{
    public class TokenAuthenticationEntity
    {
        public int UserId { get; set; }
        public string TokenKey { get; set; }
        public long ExpireDate { get; set; }
    }
}

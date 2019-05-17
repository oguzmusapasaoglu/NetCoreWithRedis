namespace NetCoreWithRedis.Domain.Authentication.Interface
{
    public interface IAuthenticationManager
    {
        bool CheckTokenAuthentication(int userId, string tokenKey);
        string CreateTokenAuthentication(int userId);
    }
}

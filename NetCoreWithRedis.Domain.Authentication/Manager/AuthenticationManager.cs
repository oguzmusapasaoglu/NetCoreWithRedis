using NetCoreWithRedis.Core.CacheManager.Redis;
using NetCoreWithRedis.Core.Helper.CommonHelper;
using NetCoreWithRedis.Core.Helper.ExceptionHelper;
using NetCoreWithRedis.Core.Log.Services;
using NetCoreWithRedis.Domain.Authentication.Entity;
using NetCoreWithRedis.Domain.Authentication.Interface;
using NetCoreWithRedis.Shared.Helper;
using System;

namespace NetCoreWithRedis.Domain.Authentication.Manager
{
    public class AuthenticationManager : RedisManager, IAuthenticationManager
    {
        private ILogManager _logger;
        public AuthenticationManager(ILogManager logger)
        {
            _logger = logger;
        }
        public bool CheckTokenAuthentication(int userId, string tokenKey)
        {
            string CacheName = ConfigManagerConst.TokenCacheName + userId;
            var TokenAuthentication = GetSingleCachedDataByName<TokenAuthenticationEntity>(CacheName);
            if (TokenAuthentication.IsNullOrEmpty())
                return false;
            if (TokenAuthentication.TokenKey != tokenKey)
                return false;
            if (TokenAuthentication.ExpireDate < DateTimeHelper.Now)
                return false;
            return true;
        }
        public string CreateTokenAuthentication(int userId)
        {
            try
            {
                string CacheName = ConfigManagerConst.TokenCacheName + userId;
                var TokenKey = Guid.NewGuid().ToString();
                long ExpireDate = DateTime.Now.AddMinutes(ConfigManager.GetData(ConfigManagerConst.TokenExpireTime).ToDouble()).ToLong();
                var Entity = new TokenAuthenticationEntity
                {
                    UserId = userId,
                    ExpireDate = ExpireDate,
                    TokenKey = TokenKey
                };
                if (IsExsistCachedDataByName<TokenAuthenticationEntity>(CacheName))
                    RemoveCachedDataSingleByName<TokenAuthenticationEntity>(CacheName);
                AddSingleCachedData(CacheName, Entity, DateTime.Now.AddMinutes(ConfigManager.GetData(ConfigManagerConst.TokenCacheTime).ToDouble()));
                return TokenKey;
            }
            catch (KnownException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                _logger.AddLog(LogTypeEnum.Error, "AuthenticationManager.CreateTokenAuthentication", userId, ex.Message, ex);
                throw new KnownException(ErrorTypeEnum.GeneralExeption, ex.Message, ex);
            }
        }
    }
}

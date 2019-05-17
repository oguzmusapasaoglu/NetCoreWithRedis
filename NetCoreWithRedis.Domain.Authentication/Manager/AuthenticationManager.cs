using NetCoreWithRedis.Core.CacheManager.Redis;
using NetCoreWithRedis.Core.Helper.CommonHelper;
using NetCoreWithRedis.Core.Helper.ExceptionHelper;
using NetCoreWithRedis.Core.Log.Interface;
using NetCoreWithRedis.Domain.Authentication.Entity;
using NetCoreWithRedis.Domain.Authentication.Interface;
using NetCoreWithRedis.Shared.Helper;
using System;

namespace NetCoreWithRedis.Domain.Authentication.Manager
{
    public class AuthenticationManager: IAuthenticationManager
    {
        private ILogService _logger;
        private IRedisManager _redisManager;
        public AuthenticationManager(ILogService logger, IRedisManager redisManager)
        {
            _logger = logger;
            _redisManager = redisManager;
        }
        public bool CheckTokenAuthentication(int userId, string tokenKey)
        {
            string CacheName = ProjectConst.TokenCacheName + userId;
            var TokenAuthentication = _redisManager.GetSingleByName<TokenAuthenticationEntity>(CacheName);
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
                string CacheName = ProjectConst.TokenCacheName + userId;
                var TokenKey = Guid.NewGuid().ToString();
                long ExpireDate = DateTime.Now.AddMinutes(ConfigManager.GetData(ProjectConst.TokenExpireTime).ToDouble()).ToLong();
                var Entity = new TokenAuthenticationEntity
                {
                    UserId = userId,
                    ExpireDate = ExpireDate,
                    TokenKey = TokenKey
                };
                if (_redisManager.IsExsistByName<TokenAuthenticationEntity>(CacheName))
                    _redisManager.RemoveSingleByName<TokenAuthenticationEntity>(CacheName);
                _redisManager.AddSingle(CacheName, Entity, DateTime.Now.AddMinutes(ConfigManager.GetData(ProjectConst.TokenCacheTime).ToDouble()));
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

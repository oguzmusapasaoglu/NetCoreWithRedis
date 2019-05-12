using NetCoreWithRedis.Core.Helper.ExceptionHelper;
using NetCoreWithRedis.Core.Log.Interface;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCoreWithRedis.Core.CacheManager.Redis
{
    public class RedisManager : IRedisManager
    {
        RedisClient client;
        string RedisIp = "";// ConfigurationManager.AppSettings[ProjectConst.RedisIp];
        int RedisPort = 0;// ConfigurationManager.AppSettings[ProjectConst.RedisPort].ToInt();
        private ILogService Logger;

        public RedisManager(ILogService logService)
        {
            this.Logger = logService;
        }

        public IEnumerable<TData> GetAll<TData>()
        {
            try
            {
                client = new RedisClient(RedisIp, RedisPort);
                var result = client.GetAll<TData>();
                return result.AsEnumerable();
            }
            catch (Exception ex)
            {
                Logger.AddCacheLog(LogTypeEnum.Error, "RedisManager.GetAll", ExceptionMessageHelper.UnexpectedCacheError, ex);
                throw new KnownException(ErrorTypeEnum.CahceGeneralException, ex.Message, ex);
            }
        }
        public void FillCache<TData>(IEnumerable<TData> entity)
        {
            try
            {
                client = new RedisClient(RedisIp, RedisPort);
                if (client.As<TData>().GetAll().Count != 0)
                    client.As<TData>().DeleteAll();

                client.StoreAll<TData>(entity);
            }
            catch (Exception ex)
            {
                Logger.AddCacheLog(LogTypeEnum.Error, "RedisManager.FillCache", ExceptionMessageHelper.UnexpectedCacheError, ex);
                throw new KnownException(ErrorTypeEnum.CahceGeneralException, ex.Message, ex);
            }
        }
        public void UpdateCache<TData>(TData entity)
        {
            try
            {
                client = new RedisClient(RedisIp, RedisPort);
                client.Store<TData>(entity);
            }
            catch (Exception ex)
            {
                Logger.AddCacheLog(LogTypeEnum.Error, "RedisManager.FillCache", ExceptionMessageHelper.UnexpectedCacheError, ex);
                throw new KnownException(ErrorTypeEnum.CahceGeneralException, ex.Message, ex);
            }
        }
        public void AddSingle<TData>(string cacheName, TData entity, DateTime expiredDate)
        {
            try
            {
                client = new RedisClient(RedisIp, RedisPort);
                client.Add<TData>(cacheName, entity, expiredDate);
            }
            catch (Exception ex)
            {
                Logger.AddCacheLog(LogTypeEnum.Error, "RedisManager.AddSingle", ExceptionMessageHelper.UnexpectedCacheError, ex);
                throw new KnownException(ErrorTypeEnum.CahceGeneralException, ex.Message, ex);
            }
        }
        public TData GetSingleByName<TData>(string cacheName)
        {
            try
            {
                client = new RedisClient(RedisIp, RedisPort);
                return client.Get<TData>(cacheName);
            }
            catch (Exception ex)
            {
                Logger.AddCacheLog(LogTypeEnum.Error, "RedisManager.cacheName", ExceptionMessageHelper.UnexpectedCacheError, ex);
                throw new KnownException(ErrorTypeEnum.CahceGeneralException, ex.Message, ex);
            }
        }
        public TData GetSingleById<TData>(int Id)
        {
            try
            {
                client = new RedisClient(RedisIp, RedisPort);
                return client.GetById<TData>(Id);
            }
            catch (Exception ex)
            {
                Logger.AddCacheLog(LogTypeEnum.Error, "RedisManager.GetSingleById", ExceptionMessageHelper.UnexpectedCacheError, ex);
                throw new KnownException(ErrorTypeEnum.CahceGeneralException, ex.Message, ex);
            }
        }
        public bool RemoveSingleByName<TData>(string cacheName)
        {
            try
            {
                client = new RedisClient(RedisIp, RedisPort);
                return client.Remove(cacheName);
            }
            catch (Exception ex)
            {
                Logger.AddCacheLog(LogTypeEnum.Error, "RedisManager.RemoveSingleByName", ExceptionMessageHelper.UnexpectedCacheError, ex);
                throw new KnownException(ErrorTypeEnum.CahceGeneralException, ex.Message, ex);
            }
        }
        public bool IsExsistByName<TData>(string cacheName)
        {
            try
            {
                client = new RedisClient(RedisIp, RedisPort);
                var AllKey = client.GetAllKeys();
                if (AllKey == null || AllKey.Count <= 0)
                    return false;

                return AllKey.Exists(q => q == cacheName);
            }
            catch (Exception ex)
            {
                Logger.AddCacheLog(LogTypeEnum.Error, "RedisManager.IsExsistByName", ExceptionMessageHelper.UnexpectedCacheError, ex);
                throw new KnownException(ErrorTypeEnum.CahceGeneralException, ex.Message, ex);
            }
        }
    }
}

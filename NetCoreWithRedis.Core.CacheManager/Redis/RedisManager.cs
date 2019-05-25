using NetCoreWithRedis.Core.Helper.CommonHelper;
using NetCoreWithRedis.Core.Helper.ExceptionHelper;
using NetCoreWithRedis.Core.Log.Services;
using NetCoreWithRedis.Shared.Helper;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreWithRedis.Core.CacheManager.Redis
{
    public abstract class RedisManager
    {
        RedisClient client;
        string RedisIp = ConfigManager.GetData(ConfigManagerConst.RedisIp);
        int RedisPort = ConfigManager.GetData(ConfigManagerConst.RedisPort).ToInt();

        public IEnumerable<TData> GetAllCachedData<TData>()
        {
            try
            {
                client = new RedisClient(RedisIp, RedisPort);
                var result = client.GetAll<TData>();
                return result.AsEnumerable();
            }
            catch (Exception ex)
            {
                throw new KnownException(ErrorTypeEnum.CahceGeneralException, "RedisManager.GetAll", ex);
            }
        }
        public void FillCacheData<TData>(IEnumerable<TData> entity)
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
                throw new KnownException(ErrorTypeEnum.CahceGeneralException, "RedisManager.FillCache", ex);
            }
        }
        public void UpdateCachedData<TData>(TData entity)
        {
            try
            {
                client = new RedisClient(RedisIp, RedisPort);
                client.Store<TData>(entity);
            }
            catch (Exception ex)
            {
                throw new KnownException(ErrorTypeEnum.CahceGeneralException, "RedisManager.FillCache", ex);
            }
        }
        public void AddSingleCachedData<TData>(string cacheName, TData entity, DateTime expiredDate)
        {
            try
            {
                client = new RedisClient(RedisIp, RedisPort);
                client.Add<TData>(cacheName, entity, expiredDate);
            }
            catch (Exception ex)
            {
                throw new KnownException(ErrorTypeEnum.CahceGeneralException, "RedisManager.AddSingle", ex);
            }
        }
        public TData GetSingleCachedDataByName<TData>(string cacheName)
        {
            try
            {
                client = new RedisClient(RedisIp, RedisPort);
                return client.Get<TData>(cacheName);
            }
            catch (Exception ex)
            {
                throw new KnownException(ErrorTypeEnum.CahceGeneralException, "RedisManager.GetSingleByName", ex);
            }
        }
        public TData GetSingleCachedDataById<TData>(int Id)
        {
            try
            {
                client = new RedisClient(RedisIp, RedisPort);
                return client.GetById<TData>(Id);
            }
            catch (Exception ex)
            {
                throw new KnownException(ErrorTypeEnum.CahceGeneralException, "RedisManager.GetSingleById", ex);
            }
        }
        public bool RemoveCachedDataSingleByName<TData>(string cacheName)
        {
            try
            {
                client = new RedisClient(RedisIp, RedisPort);
                return client.Remove(cacheName);
            }
            catch (Exception ex)
            {
                throw new KnownException(ErrorTypeEnum.CahceGeneralException, "RedisManager.RemoveSingleByName", ex);
            }
        }
        public bool IsExsistCachedDataByName<TData>(string cacheName)
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
                throw new KnownException(ErrorTypeEnum.CahceGeneralException, "RedisManager.IsExsistByName", ex);
            }
        }
    }
}

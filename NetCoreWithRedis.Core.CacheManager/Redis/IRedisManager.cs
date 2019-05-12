using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreWithRedis.Core.CacheManager.Redis
{
    public interface IRedisManager
    {
        IEnumerable<TData> GetAll<TData>();
        void FillCache<TData>(IEnumerable<TData> entity);
        void UpdateCache<TData>(TData entity);
        void AddSingle<TData>(string cacheName, TData entity, DateTime expiredDate);
        TData GetSingleByName<TData>(string cacheName);
        TData GetSingleById<TData>(int Id);
        bool RemoveSingleByName<TData>(string cacheName);
        bool IsExsistByName<TData>(string cacheName);
    }
}

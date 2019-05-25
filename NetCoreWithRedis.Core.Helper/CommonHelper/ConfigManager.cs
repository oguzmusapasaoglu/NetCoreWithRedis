using System.Configuration;

namespace NetCoreWithRedis.Core.Helper.CommonHelper
{
    public class ConfigManager
    {
        public static string GetData(string Key) => ConfigurationManager.AppSettings.Get(Key);
    }
    public class ConfigManagerConst
    {
        public static string TokenCacheName = "TokenCache";
        public static string TokenExpireTime = "TokenExpireTime";
        public static string TokenCacheTime = "TokenCacheTime";
        public static string ConnStr = "ConnStr";
        public static string RedisIp = "RedisIp";
        public static string RedisPort = "RedisPort";
    }
}

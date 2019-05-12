using System.Configuration;

namespace NetCoreWithRedis.Core.Helper.CommonHelper
{
    public class ConfigManager
    {
        public static string GetData(string Key) => ConfigurationManager.AppSettings.Get(Key);
    }
    public class ConfigKey
    {
        public const string TokenTime = "TokenTime";
        public const string ConnStr = "TokenTime";
    }
}

using System.Configuration;

namespace NetCoreWithRedis.Core.Helper.CommonHelper
{
    public class ConfigManager
    {
        public static string GetData(string Key) => ConfigurationManager.AppSettings.Get(Key);
    }
}

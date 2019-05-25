using Dapper.Contrib.Extensions;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace NetCoreWithRedis.Core.Helper.CommonHelper
{
    public class AttiributeHelper
    {
        //private static readonly ConcurrentDictionary<RuntimeTypeHandle, string> TypeTableName = new ConcurrentDictionary<RuntimeTypeHandle, string>();

        //public static string GetTableName(Type type)
        //{
        //    string name;
        //    if (!TypeTableName.TryGetValue(type.TypeHandle, out name))
        //    {
        //        var tableattr = type.GetCustomAttributes(false).Where(attr => attr.GetType().Name == "TableAttribute").SingleOrDefault() as
        //           dynamic;
        //        if (tableattr != null)
        //            name = tableattr.Name;
        //        TypeTableName[type.TypeHandle] = name;
        //    }
        //    return name;
        //}

        public static string GetTableName(Type type)
        {
            dynamic tableattr = type.GetCustomAttributes(false).SingleOrDefault(attr => attr.GetType().Name == "TableAttribute");
            var name = string.Empty;
            if (tableattr != null)
            {
                name = tableattr.Name;
            }
            return name;
        }

        public static string GetTableName<T>(Type type)
        {
            var tAttribute =
     (TableAttribute)typeof(T).GetCustomAttributes(typeof(TableAttribute), true)[0];
            string tableName = tAttribute.Name;
            return tableName;
        }
    }
}

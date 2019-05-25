using System.Data;
using System.Collections.Generic;
using NetCoreWithRedis.Core.Helper.EntityHelper;

namespace NetCoreWithRedis.Core.Helper.QuerryManager
{
    public static class DynamicQuerryParameters
    {
        public static IList<ParmInfo> Create()
        {
            ParmInfoList = new List<ParmInfo>();
            return ParmInfoList;
        }
        public static IList<ParmInfo> ParmInfoList { get; set; }
        public static ParmInfo AddParameter(string parmName, object parmValue, QueryOperatorType parmQueryOperator, LogicalOperatorType parmLogicalOperator, DbType parmDbType, ParameterDirection parmParameterDirection = ParameterDirection.Input)
        {
            return new ParmInfo
            {
                Name = parmName,
                DbType = parmDbType,
                ParameterDirection = parmParameterDirection,
                DataValue = parmValue,
                LogicalOperator = parmLogicalOperator,
                QueryOperator = QueryOperatorType.Equals
            };
        }
        public static ParmInfo AddParameter(string parmName, object parmValue, DbType parmDbType, ParameterDirection parmParameterDirection = ParameterDirection.Input)
        {
            return new ParmInfo
            {
                Name = parmName,
                DbType = parmDbType,
                ParameterDirection = parmParameterDirection,
                DataValue = parmValue,
                LogicalOperator = LogicalOperatorType.AND,
                QueryOperator = QueryOperatorType.Equals
            };
        }
        public static void Clear()
        {
            if (ParmInfoList != null)
                ParmInfoList.Clear();
        }
    }
}

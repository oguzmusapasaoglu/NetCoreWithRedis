using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace NetCoreWithRedis.Core.Helper.EntityHelper
{
    public class ParmInfo
    {
        public string Name { get; set; }
        public object DataValue { get; set; }
        public ParameterDirection ParameterDirection { get; set; }
        public DbType? DbType { get; set; }
        public int? Size { get; set; }
        public LogicalOperatorType? LogicalOperator { get; set; }
        public QueryOperatorType? QueryOperator { get; set; }
    }
}

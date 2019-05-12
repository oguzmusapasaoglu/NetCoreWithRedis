using System;

namespace NetCoreWithRedis.Core.Helper.CommonHelper
{
    public static class QueryExtensions
    {
        public static string ConvertToSqlOperator(this QueryOperatorType oOperator)
        {
            switch (oOperator)
            {
                case QueryOperatorType.Equals:
                    return "=";
                case QueryOperatorType.NotEquals:
                    return "<>";
                case QueryOperatorType.GreaterThan:
                    return ">";
                case QueryOperatorType.GreaterOrEquals:
                    return ">=";
                case QueryOperatorType.LessThan:
                    return "<";
                case QueryOperatorType.LessOrEquals:
                    return "<=";
                case QueryOperatorType.Like:
                    return "LIKE";
                case QueryOperatorType.NotLike:
                    return "NOT LIKE";
                default:
                    throw new ArgumentOutOfRangeException("Operator");
            }
        }
    }
}

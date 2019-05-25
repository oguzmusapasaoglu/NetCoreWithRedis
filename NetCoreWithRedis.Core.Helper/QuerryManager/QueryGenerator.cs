using NetCoreWithRedis.Core.Helper.CommonHelper;
using NetCoreWithRedis.Core.Helper.EntityHelper;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCoreWithRedis.Core.Helper.QuerryManager
{
    public class QueryGenerator
    {
        public static string tableName<T>()
        {
            var type = typeof(T);
            string tableName = AttiributeHelper.GetTableName(type);
            return tableName;
        }
        public static GeneratedQuery generated { get; set; }
        public static GeneratedQuery GenerateQuery(IList<ParmInfo> ParmInfoList, GeneratedQuery query)
        {
            generated = new GeneratedQuery();
            GenerateWhereScript(ParmInfoList);
            generated.QueryScript.Append(generated);
            return generated;
        }
        public static GeneratedQuery GenerateQuery(string TableName, ParmInfo _ParmInfo = null)
        {
            var list = new List<ParmInfo>();
            if (_ParmInfo != null)
            {
                list.Add(_ParmInfo);
                GenerateWhereScript(list);
            }
            generated = new GeneratedQuery();
            GenerateSelectForSingle();
            GenerateFromForSingle(TableName);
            generated.QueryScript.Append(generated.SelectClause);
            generated.QueryScript.Append(generated.FromClause);
            generated.QueryScript.Append(generated.WhereClause);
            return generated;
        }
        public static GeneratedQuery GenerateQuery(IList<ParmInfo> ParmInfoList, string TableName)
        {
            generated = new GeneratedQuery();
            GenerateSelectForSingle();
            GenerateFromForSingle(TableName);
            GenerateWhereScript(ParmInfoList);
            generated.QueryScript.Append(generated.SelectClause);
            generated.QueryScript.Append(generated.FromClause);
            generated.QueryScript.Append(generated.WhereClause);
            return generated;
        }
        private static void GenerateSelectForSingle()
        {
            generated.SelectClause.AppendFormat(" SELECT T.* ").AppendLine();
        }
        private static void GenerateFromForSingle(string parmTableName)
        {
            generated.FromClause.AppendFormat(" FROM {0} T ", parmTableName).AppendLine();
        }
        private static void GenerateWhereScript(IList<ParmInfo> ParmInfoList)
        {
            generated.Parameters = new Dapper.DynamicParameters();
            generated.WhereClause.Clear();
            generated.WhereClause.Append(" WHERE ");
            var first = ParmInfoList.First();
            foreach (var pair in ParmInfoList)
            {
                var sb = new StringBuilder();
                string Operator = pair.QueryOperator.Value.ConvertToSqlOperator();
                if (pair.QueryOperator != null && pair.LogicalOperator != null)
                {
                    if (pair.QueryOperator.Value != QueryOperatorType.Like && pair.QueryOperator.Value != QueryOperatorType.NotLike)
                        sb = (pair == first)
                            ? generated.WhereClause.AppendFormat(" T.{0} {1} @{0} ", pair.Name, Operator)
                            : generated.WhereClause.AppendFormat("{2} T.{0} {1} @{0} ", pair.Name, Operator, pair.LogicalOperator);
                    else
                        generated.WhereClause.AppendFormat(" T.{0} {1} '%@{0}%' ", pair.Name, Operator);
                }
                else
                    generated.WhereClause.AppendFormat("AND T.{0} = @{0} ", pair.Name);

                generated.Parameters.Add(pair.Name, pair.DataValue, pair.DbType, pair.ParameterDirection, pair.Size);
            }
        }

        public static GeneratedQuery GeneratedPagingQuery(string TableName, int PageNumber, int RowsPerPage, IList<ParmInfo> ParmInfoList)
        {
            generated = new GeneratedQuery();
            generated.SelectClause.AppendFormat("SELECT *, TotalRowCount = COUNT(*) OVER() FROM {0} ", TableName);
            GenerateWhereScript(ParmInfoList);
            generated.SelectClause.Append(generated.WhereClause);
            generated.SelectClause.AppendFormat(" ORDER BY Id OFFSET({0} - 1) * {1} ROWS FETCH NEXT {1} ROWS ONLY", PageNumber, RowsPerPage);
            return generated;
        }
    }
}

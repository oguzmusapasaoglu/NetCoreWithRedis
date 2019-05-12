using System.Text;
using Dapper;

namespace NetCoreWithRedis.Core.Helper.CommonHelper
{
    public class GeneratedQuery
    {
        public GeneratedQuery()
        {
            Parameters = new DynamicParameters();
            QueryScript = new StringBuilder();
            SelectClause = new StringBuilder();
            FromClause = new StringBuilder();
            WhereClause = new StringBuilder();
            OrderbyClause = new StringBuilder();
            GroupbyClause = new StringBuilder();
            InsertClause = new StringBuilder();
        }

        public string TableName { get; set; }
        public StringBuilder QueryScript { get; set; }
        public StringBuilder SelectClause { get; set; }
        public StringBuilder FromClause { get; set; }
        public StringBuilder WhereClause { get; set; }
        public StringBuilder OrderbyClause { get; set; }
        public StringBuilder GroupbyClause { get; set; }
        public StringBuilder InsertClause { get; set; }
        public DynamicParameters Parameters { get; set; }
    }
}

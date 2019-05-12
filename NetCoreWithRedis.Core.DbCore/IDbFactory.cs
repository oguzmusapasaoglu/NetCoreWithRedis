using NetCoreWithRedis.Core.Helper.CommonHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace NetCoreWithRedis.Core.DbCore
{
    public interface IDbFactory
    {
        IDbTransaction _DbTransaction { get; set; }
        DbConnection CreateConnection(bool parmNeedTrans = true);
        void CloseConnection(DbConnection conn);
        TResult InsertOrUpdate<TResult>(GeneratedQuery InsertQuerry, object data);
        TResult InsertOrUpdate<TResult>(GeneratedQuery query);
        TResult InsertOrUpdateWithSp<TResult>(string SpName, object parm);
        IEnumerable<TResult> GetData<TResult>(GeneratedQuery querry);
        IEnumerable<TResult> GetData<TResult>(GeneratedQuery querry, object parm);
        IEnumerable<TResult> GetDataWithSp<TResult>(string SpName, object parm);
        TResult GetSingleData<TResult>(GeneratedQuery querry);
        TResult GetSingleDataWithSp<TResult>(string SpName, object parm);
        IEnumerable<TResponse> GetDataPaging<TResponse>(int parmPageNumber, int parmRowPerPage, GeneratedQuery parmGeneratedPagingQuery);
    }
}

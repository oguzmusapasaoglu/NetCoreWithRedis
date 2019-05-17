using NetCoreWithRedis.Shared.DTO.Response.Base;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreWithRedis.Core.Helper.CommonHelper
{
    public class PageingHelper
    {
        public static PagingResultDto<IEnumerable<TData>> GetPagingResult<TData>(IEnumerable<TData> data, int PageNumber, int PageSize)
        {
            try
            {
                var TotalItemCount = data.Count();
                var Result = new PagingResultDto<IEnumerable<TData>>();
                Result.CurrPage = PageNumber;
                Result.TotalPage = (TotalItemCount % PageSize == 0
                           ? TotalItemCount / PageSize
                           : (TotalItemCount / PageSize) + 1);
                Result.Response = data.Skip(PageSize * PageNumber).Take(PageSize);
                return Result;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}

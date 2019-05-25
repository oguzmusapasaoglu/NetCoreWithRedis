using NetCoreWithRedis.Shared.DTO;
using NetCoreWithRedis.Shared.DTO.Request;
using NetCoreWithRedis.Shared.DTO.Response.Base;
using System.Collections.Generic;

namespace NetCoreWithRedis.Domain.Product.Interface
{
    public interface IProductManager
    {
        #region Product Type
        ProductTypeDto CreateOrUpdateProductType(ProductTypeDto dto, int RequestUserId, string TokenKey);
        IEnumerable<ProductTypeDto> GetAllProductType(ProductTypeDto request, int RequestUserId, string TokenKey);
        ProductTypeDto GetSingleProductType(int id, int RequestUserId, string TokenKey);
        ProductTypeDto GetSingleProductType(ProductTypeDto request, int RequestUserId, string TokenKey);
        #endregion

        #region Product Immage
        ProductImageDto CreateOrUpdateProductImage(ProductImageDto dto, int RequestUserId, string TokenKey);
        IEnumerable<ProductImageDto> GetAllProductImage(ProductImageDto request, int RequestUserId, string TokenKey);
        ProductImageDto GetSingleProductImage(int id, int RequestUserId, string TokenKey);
        ProductImageDto GetSingleProductImage(ProductImageDto request, int RequestUserId, string TokenKey);
        #endregion

    }
}

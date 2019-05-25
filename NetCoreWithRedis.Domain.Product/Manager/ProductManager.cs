using Dapper.Contrib.Extensions;
using NetCoreWithRedis.Core.DbCore;
using NetCoreWithRedis.Core.Helper.CommonHelper;
using NetCoreWithRedis.Core.Helper.ExceptionHelper;
using NetCoreWithRedis.Core.Log.Services;
using NetCoreWithRedis.Domain.Authentication.Interface;
using NetCoreWithRedis.Domain.Product.Entity;
using NetCoreWithRedis.Domain.Product.Interface;
using NetCoreWithRedis.Shared.DTO;
using NetCoreWithRedis.Shared.DTO.Request;
using NetCoreWithRedis.Shared.DTO.Response.Base;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using NetCoreWithRedis.Core.CacheManager.Redis;

namespace NetCoreWithRedis.Domain.Product.Manager
{
    public class ProductManager : RedisManager, IProductManager
    {
        #region Constructor
        private IDbFactory Db;
        private IAuthenticationManager Authentication;
        private ILogManager Logger;

        public ProductManager(
            ILogManager logger,
            IDbFactory db,
            IAuthenticationManager authentication)
        {
            Logger = logger;
            Db = db;
            Authentication = authentication;
        }
        #endregion

        #region ProductType
        public ProductTypeDto CreateOrUpdateProductType(ProductTypeDto dto, int RequestUserId, string TokenKey)
        {
            try
            {
                CheckAuthentication(RequestUserId, TokenKey);

                #region Empty Control
                if (dto.Name.IsNullOrEmpty())
                    throw new RequestWarningException(ErrorTypeEnum.WarningException, ExceptionCodeHelper.CannotEmptyField, ExceptionMessageHelper.CannotEmptyField("Name"));
                #endregion

                var data = GetAllCachedData<ProductTypeEntity>().ToList();

                #region Field Control
                if (data != null)
                {
                    if (data.Any(q => q.Name == dto.Name))
                        throw new RequestWarningException(ErrorTypeEnum.WarningException, ExceptionCodeHelper.IsInUse, ExceptionMessageHelper.IsInUse("Name"));
                }
                #endregion

                var entity = dto.ConvertTo<ProductTypeEntity>();
                var conn = Db.CreateConnection(true);
                if (dto.Id > 0)
                {
                    entity.UpdateUser = RequestUserId;
                    entity.UpdateDate = DateTimeHelper.Now;
                    conn.Update(entity, Db._DbTransaction);
                    data.RemoveAt(data.FindIndex(q => q.Id == entity.Id));
                }
                else
                {
                    int Id = conn.Insert(entity, Db._DbTransaction).ToInt();
                    entity = conn.Get<ProductTypeEntity>(Id);
                }
                data.Add(entity);
                FillCacheData(data);
                return entity.ConvertTo<ProductTypeDto>();
            }
            catch (KnownException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.AddLog(LogTypeEnum.Error, "ProductManager.CreateOrUpdateProductType", RequestUserId, ex.Message, dto.ToJson(), ex);
                throw new KnownException(ErrorTypeEnum.UnexpectedExeption, ex.Message, ex);
            }
        }
        public IEnumerable<ProductTypeDto> GetAllProductType(ProductTypeDto request, int RequestUserId, string TokenKey)
        {
            try
            {
                CheckAuthentication(RequestUserId, TokenKey);
                var data = GetAllCachedData<ProductTypeEntity>().AsQueryable();
                var predicate = PredicateBuilderHelper.True<ProductTypeEntity>();
                if (request.Id.HasValue)
                    predicate = predicate.And(q => q.Id == request.Id);
                if (request.ActivationStatus > 0)
                    predicate = predicate.And(q => q.ActivationStatus == request.ActivationStatus);
                if (!request.Name.IsNullOrEmpty())
                    predicate = predicate.And(q => q.Name.Contains(request.Name));
                var result = data.Where(predicate).AsEnumerable();
                return result.ConvertTo<IEnumerable<ProductTypeDto>>();
            }
            catch (KnownException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.AddLog(LogTypeEnum.Error, "ProductManager.GetAllProductType", RequestUserId, ex.Message, request.ToJson(), ex);
                throw new KnownException(ErrorTypeEnum.UnexpectedExeption, ex.Message, ex);
            }
        }
        public ProductTypeDto GetSingleProductType(int id, int RequestUserId, string TokenKey)
        {
            try
            {
                CheckAuthentication(RequestUserId, TokenKey);
                var data = GetAllCachedData<ProductTypeEntity>();
                if (data.IsNullOrEmpty())
                    return null;
                var result = data.FirstOrDefault(q => q.Id == id);
                return result.ConvertTo<ProductTypeDto>();
            }
            catch (KnownException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.AddLog(LogTypeEnum.Error, "ProductManager.GetSingleProductType", RequestUserId, ex.Message, ex);
                throw new KnownException(ErrorTypeEnum.UnexpectedExeption, ex.Message, ex);
            }
        }
        public ProductTypeDto GetSingleProductType(ProductTypeDto request, int RequestUserId, string TokenKey)
        {
            try
            {
                CheckAuthentication(RequestUserId, TokenKey);
                var data = GetAllCachedData<ProductTypeEntity>().AsQueryable();
                var predicate = PredicateBuilderHelper.True<ProductTypeEntity>();
                if (request.Id.HasValue)
                    predicate = predicate.And(q => q.Id == request.Id);
                if (request.ActivationStatus > 0)
                    predicate = predicate.And(q => q.ActivationStatus == request.ActivationStatus);
                if (!request.Name.IsNullOrEmpty())
                    predicate = predicate.And(q => q.Name.Contains(request.Name));
                var result = data.FirstOrDefault(predicate);
                return result.ConvertTo<ProductTypeDto>();
            }
            catch (KnownException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.AddLog(LogTypeEnum.Error, "ProductManager.GetSingleProductType", null, ex.Message, request.ToJson(), ex);
                throw new KnownException(ErrorTypeEnum.UnexpectedExeption, ex.Message, ex);
            }
        }
        #endregion

        #region ProductImage
        public ProductImageDto CreateOrUpdateProductImage(ProductImageDto dto, int RequestUserId, string TokenKey)
        {
            try
            {
                CheckAuthentication(RequestUserId, TokenKey);

                #region Empty Control
                if (dto.ImageUrl.IsNullOrEmpty())
                    throw new RequestWarningException(ErrorTypeEnum.WarningException, ExceptionCodeHelper.CannotEmptyField, ExceptionMessageHelper.CannotEmptyField("ImageUrl"));
                #endregion

                var data = GetAllCachedData<ProductTypeEntity>().ToList();
                var entity = dto.ConvertTo<ProductTypeEntity>();
                var conn = Db.CreateConnection(true);
                if (dto.Id > 0)
                {
                    entity.UpdateUser = RequestUserId;
                    entity.UpdateDate = DateTimeHelper.Now;
                    conn.Update(entity, Db._DbTransaction);
                    data.RemoveAt(data.FindIndex(q => q.Id == entity.Id));
                }
                else
                {
                    int Id = conn.Insert(entity, Db._DbTransaction).ToInt();
                    entity = conn.Get<ProductTypeEntity>(Id);
                }
                data.Add(entity);
                FillCacheData(data);
                return entity.ConvertTo<ProductImageDto>();
            }
            catch (KnownException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.AddLog(LogTypeEnum.Error, "ProductManager.CreateOrUpdateProductImage", RequestUserId, ex.Message, dto.ToJson(), ex);
                throw new KnownException(ErrorTypeEnum.UnexpectedExeption, ex.Message, ex);
            }
        }
        public IEnumerable<ProductImageDto> GetAllProductImage(ProductImageDto request, int RequestUserId, string TokenKey)
        {
            try
            {
                CheckAuthentication(RequestUserId, TokenKey);
                var data = GetAllCachedData<ProductImageEntity>().Where(q => q.ProductId == request.ProductId).AsQueryable();
                var predicate = PredicateBuilderHelper.True<ProductImageEntity>();
                if (request.Id.HasValue)
                    predicate = predicate.And(q => q.Id == request.Id);
                if (request.ActivationStatus > 0)
                    predicate = predicate.And(q => q.ActivationStatus == request.ActivationStatus);
                if (!request.ImageUrl.IsNullOrEmpty())
                    predicate = predicate.And(q => q.ImageUrl.Contains(request.ImageUrl));
                var result = data.Where(predicate).AsEnumerable();
                return result.ConvertTo<IEnumerable<ProductImageDto>>();
            }
            catch (KnownException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.AddLog(LogTypeEnum.Error, "ProductManager.GetAllProductImage", RequestUserId, ex.Message, request.ToJson(), ex);
                throw new KnownException(ErrorTypeEnum.UnexpectedExeption, ex.Message, ex);
            }
        }
        public ProductImageDto GetSingleProductImage(int id, int RequestUserId, string TokenKey)
        {
            try
            {
                CheckAuthentication(RequestUserId, TokenKey);
                var data = GetAllCachedData<ProductImageEntity>();
                if (data.IsNullOrEmpty())
                    return null;
                var result = data.FirstOrDefault(q => q.Id == id);
                return result.ConvertTo<ProductImageDto>();
            }
            catch (KnownException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.AddLog(LogTypeEnum.Error, "ProductManager.GetSingleProductImage", RequestUserId, ex.Message, ex);
                throw new KnownException(ErrorTypeEnum.UnexpectedExeption, ex.Message, ex);
            }
        }
        public ProductImageDto GetSingleProductImage(ProductImageDto request, int RequestUserId, string TokenKey)
        {
            try
            {
                CheckAuthentication(RequestUserId, TokenKey);
                var data = GetAllCachedData<ProductImageEntity>().AsQueryable();
                var predicate = PredicateBuilderHelper.True<ProductImageEntity>();
                if (request.Id.HasValue)
                    predicate = predicate.And(q => q.Id == request.Id);
                if (request.ActivationStatus > 0)
                    predicate = predicate.And(q => q.ActivationStatus == request.ActivationStatus);
                if (!request.ImageUrl.IsNullOrEmpty())
                    predicate = predicate.And(q => q.ImageUrl.Contains(request.ImageUrl));
                var result = data.FirstOrDefault(predicate);
                return result.ConvertTo<ProductImageDto>();
            }
            catch (KnownException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.AddLog(LogTypeEnum.Error, "ProductManager.GetSingleProductImage", null, ex.Message, request.ToJson(), ex);
                throw new KnownException(ErrorTypeEnum.UnexpectedExeption, ex.Message, ex);
            }
        }
        #endregion

        #region Product
        public ProductDto CreateOrUpdateProduct(ProductDto dto, int RequestUserId, string TokenKey)
        {
            try
            {
                CheckAuthentication(RequestUserId, TokenKey);

                #region Empty Control
                if (dto.Name.IsNullOrEmpty())
                    throw new RequestWarningException(ErrorTypeEnum.WarningException, ExceptionCodeHelper.CannotEmptyField, ExceptionMessageHelper.CannotEmptyField("Name"));
                #endregion

                var data = GetAllCachedData<ProductEntity>().ToList();
                #region Field Control
                if (data != null)
                {
                    if (data.Any(q => q.Name == dto.Name && q.ProductTypeId == dto.ProductTypeId))
                        throw new RequestWarningException(ErrorTypeEnum.WarningException, ExceptionCodeHelper.IsInUse, ExceptionMessageHelper.IsInUse("Name"));
                }
                #endregion

                var entity = dto.ConvertTo<ProductEntity>();
                var conn = Db.CreateConnection(true);
                if (dto.Id > 0)
                {
                    entity.UpdateUser = RequestUserId;
                    entity.UpdateDate = DateTimeHelper.Now;
                    conn.Update(entity, Db._DbTransaction);
                    data.RemoveAt(data.FindIndex(q => q.Id == entity.Id));
                }
                else
                {
                    int Id = conn.Insert(entity, Db._DbTransaction).ToInt();
                    entity = conn.Get<ProductEntity>(Id);
                }
                data.Add(entity);
                FillCacheData(data);
                return entity.ConvertTo<ProductDto>();
            }
            catch (KnownException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.AddLog(LogTypeEnum.Error, "ProductManager.CreateOrUpdateProduct", RequestUserId, ex.Message, dto.ToJson(), ex);
                throw new KnownException(ErrorTypeEnum.UnexpectedExeption, ex.Message, ex);
            }
        }
        public PagingResultDto<IEnumerable<ProductDto>> GetAllProduct(ProductFilterRequestDto request, int RequestUserId, string TokenKey)
        {
            try
            {
                CheckAuthentication(RequestUserId, TokenKey);
                var data = GetAllCachedData<ProductEntity>().AsQueryable();
                var predicate = PredicateBuilderHelper.True<ProductEntity>();
                if (request.PriceStart.HasValue && request.PriceEnd.HasValue)
                    predicate = predicate.And(q => q.Price >= request.PriceStart && q.Price <= request.PriceEnd);
                if (request.DiscountPriceStart.HasValue && request.DiscountPriceEnd.HasValue)
                    predicate = predicate.And(q => q.DiscountPrice >= request.DiscountPriceStart && q.DiscountPrice <= request.DiscountPriceEnd);
                if (request.ProductTypeId.HasValue)
                    predicate = predicate.And(q => q.ProductTypeId == request.ProductTypeId.Value);
                var result = data.Where(predicate).AsEnumerable();
                var rdata = result.ConvertTo<IEnumerable<ProductDto>>();
                var ReturnData = PageingHelper.GetPagingResult(rdata, request.PageNumber, request.PageSize);
                return ReturnData;
            }
            catch (KnownException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.AddLog(LogTypeEnum.Error, "ProductManager.GetAllProduct", RequestUserId, ex.Message, request.ToJson(), ex);
                throw new KnownException(ErrorTypeEnum.UnexpectedExeption, ex.Message, ex);
            }
        }

        public ProductDto GetSingleProduct(int id, int RequestUserId, string TokenKey)
        {
            try
            {
                CheckAuthentication(RequestUserId, TokenKey);
                var data = GetAllCachedData<ProductEntity>();
                if (data.IsNullOrEmpty())
                    return null;
                var result = data.FirstOrDefault(q => q.Id == id);
                return result.ConvertTo<ProductDto>();
            }
            catch (KnownException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.AddLog(LogTypeEnum.Error, "ProductManager.GetSingleProduct", RequestUserId, ex.Message, ex);
                throw new KnownException(ErrorTypeEnum.UnexpectedExeption, ex.Message, ex);
            }
        }
        public ProductDto GetSingleProduct(ProductDto request, int RequestUserId, string TokenKey)
        {
            try
            {
                CheckAuthentication(RequestUserId, TokenKey);
                var data = GetAllCachedData<ProductEntity>().AsQueryable();
                var predicate = PredicateBuilderHelper.True<ProductEntity>();
                if (request.Id.HasValue)
                    predicate = predicate.And(q => q.Id == request.Id);
                if (request.ActivationStatus > 0)
                    predicate = predicate.And(q => q.ActivationStatus == request.ActivationStatus);
                var result = data.FirstOrDefault(predicate);
                return result.ConvertTo<ProductDto>();
            }
            catch (KnownException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.AddLog(LogTypeEnum.Error, "ProductManager.GetSingleProduct", null, ex.Message, request.ToJson(), ex);
                throw new KnownException(ErrorTypeEnum.UnexpectedExeption, ex.Message, ex);
            }
        }
        #endregion

        private void CheckAuthentication(int RUserId, string TKey)
        {
            if (!Authentication.CheckTokenAuthentication(RUserId, TKey))
            {
                Logger.AddLog(LogTypeEnum.Warn, "ProductManager.CheckAuthentication", RUserId, ExceptionMessageHelper.UnauthorizedAccess(RUserId), string.Empty);
                throw new KnownException(ErrorTypeEnum.AuthorizeException, ExceptionMessageHelper.UnauthorizedAccess(RUserId), string.Empty);
            }
        }
    }
}

using Dapper.Contrib.Extensions;
using NetCoreWithRedis.Core.CacheManager.Redis;
using NetCoreWithRedis.Core.DbCore;
using NetCoreWithRedis.Core.Helper.CommonHelper;
using NetCoreWithRedis.Core.Helper.ExceptionHelper;
using NetCoreWithRedis.Core.Log.Services;
using NetCoreWithRedis.Domain.Authentication.Interface;
using NetCoreWithRedis.Domain.Users.Entity;
using NetCoreWithRedis.Domain.Users.Interface;
using NetCoreWithRedis.Shared.DTO;
using NetCoreWithRedis.Shared.DTO.Request;
using NetCoreWithRedis.Shared.DTO.Response.Base;
using NetCoreWithRedis.Shared.Entities;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCoreWithRedis.Domain.Users.Manager
{
    public class CustomerManager : RedisManager, ICustomerManager
    {
        private ILogManager Logger;
        private IDbFactory Db;
        private IAuthenticationManager Authentication;

        public CustomerManager(
            ILogManager logger,
            IDbFactory db,
            IAuthenticationManager authentication)
        {
            Logger = logger;
            Db = db;
            Authentication = authentication;
        }
        #region Customer
        public CustomerDto CreateOrUpdateCustomer(CustomerDto dto, int RequestUserId, string TokenKey)
        {
            try
            {
                CheckAuthentication(RequestUserId, TokenKey);
                #region Empty Control
                if (dto.UserName.IsNullOrEmpty())
                    throw new RequestWarningException(ErrorTypeEnum.WarningException, ExceptionCodeHelper.CannotEmptyField, ExceptionMessageHelper.CannotEmptyField("User Name"));
                if (dto.Password.IsNullOrEmpty())
                    throw new RequestWarningException(ErrorTypeEnum.WarningException, ExceptionCodeHelper.CannotEmptyField, ExceptionMessageHelper.CannotEmptyField("Password"));
                if (dto.FullName.IsNullOrEmpty())
                    throw new RequestWarningException(ErrorTypeEnum.WarningException, ExceptionCodeHelper.CannotEmptyField, ExceptionMessageHelper.CannotEmptyField("Full Name"));
                #endregion

                var data = GetAllCachedData<CustomerDto>().ToList();

                #region Field Control
                if (data != null)
                    if (data.Any(q => q.UserName == dto.UserName))
                        throw new RequestWarningException(ErrorTypeEnum.WarningException, ExceptionCodeHelper.IsInUse, ExceptionMessageHelper.IsInUse("UserName"));
                #endregion

                var entity = dto.ConvertTo<CustomerEntity>();
                var Pass = PasswordHelper.GeneratePassword(6);
                entity.Password = (dto.Id > 0)
                    ? PasswordHelper.EncryptData(dto.Password)
                    : PasswordHelper.EncryptData(Pass);
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
                    entity = conn.Get<CustomerEntity>(Id);
                }
                var result = entity.ConvertTo<CustomerDto>();
                data.Add(result);
                FillCacheData(data);
                return result;
            }
            catch (KnownException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.AddLog(LogTypeEnum.Error, "CustomerManager.CreateOrUpdateCustomer", RequestUserId, ex.Message, dto.ToJson(), ex);
                throw new KnownException(ErrorTypeEnum.UnexpectedExeption, ex.Message, ex);
            }
        }
        public PagingResultDto<IEnumerable<CustomerDto>> GetAllCustomer(CustomerFilterRequestDto request, int RequestUserId, string TokenKey)
        {
            try
            {
                CheckAuthentication(RequestUserId, TokenKey);
                var data = GetAllCachedData<CustomerDto>().AsQueryable();
                var predicate = PredicateBuilderHelper.True<CustomerDto>();
                if (request.Id.HasValue)
                    predicate = predicate.And(q => q.Id == request.Id);
                if (request.ActivationStatus.HasValue)
                    predicate = predicate.And(q => q.ActivationStatus == request.ActivationStatus);
                if (!request.UserName.IsNullOrEmpty())
                    predicate = predicate.And(q => q.UserName.Contains(request.UserName));
                if (!request.FullName.IsNullOrEmpty())
                    predicate = predicate.And(q => q.FullName.Contains(request.FullName));
                var result = data.Where(predicate).AsEnumerable();
                var ReturnData = PageingHelper.GetPagingResult(result, request.PageNumber, request.PageSize);
                return ReturnData;
            }
            catch (KnownException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.AddLog(LogTypeEnum.Error, "CustomerManager.GetAllCustomer", RequestUserId, ex.Message, request.ToJson(), ex);
                throw new KnownException(ErrorTypeEnum.UnexpectedExeption, ex.Message, ex);
            }
        }
        public CustomerDto GetSingleCustomer(int id, int RequestUserId, string TokenKey)
        {
            try
            {
                CheckAuthentication(RequestUserId, TokenKey);
                var data = GetAllCachedData<CustomerDto>();
                var result = data.FirstOrDefault(q => q.Id == id);
                return result;
            }
            catch (KnownException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.AddLog(LogTypeEnum.Error, "CustomerManager.GetSingleCustomer", RequestUserId, ex.Message, ex);
                throw new KnownException(ErrorTypeEnum.UnexpectedExeption, ex.Message, ex);
            }
        }
        public CustomerDto GetSingleCustomer(CustomerDto request)
        {
            try
            {
                var data = GetAllCachedData<CustomerDto>().AsQueryable();
                var predicate = PredicateBuilderHelper.True<CustomerDto>();
                if (request.Id.HasValue)
                    predicate = predicate.And(q => q.Id == request.Id);
                predicate = predicate.And(q => q.ActivationStatus == (int)ActivationStatusType.Active);
                if (!request.UserName.IsNullOrEmpty())
                    predicate = predicate.And(q => q.UserName.Contains(request.UserName));
                if (!request.FullName.IsNullOrEmpty())
                    predicate = predicate.And(q => q.FullName.Contains(request.FullName));
                var result = data.Where(predicate).AsEnumerable().FirstOrDefault();
                return result;
            }
            catch (KnownException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.AddLog(LogTypeEnum.Error, "CustomerManager.GetSingleCustomer", null, ex.Message, request.ToJson(), ex);
                throw new KnownException(ErrorTypeEnum.UnexpectedExeption, ex.Message, ex);
            }
        }
        #endregion

        #region Customer Adress
        public CustomerAddressDto CreateOrUpdateCustomerAddress(CustomerAddressDto dto, int RequestUserId, string TokenKey)
        {
            try
            {
                CheckAuthentication(RequestUserId, TokenKey);
                #region Empty Control
                if (dto.CustomerId.IsNullOrEmpty())
                    throw new RequestWarningException(ErrorTypeEnum.WarningException, ExceptionCodeHelper.CannotEmptyField, ExceptionMessageHelper.CannotEmptyField("Customer Id"));
                if (dto.AddressTitle.IsNullOrEmpty())
                    throw new RequestWarningException(ErrorTypeEnum.WarningException, ExceptionCodeHelper.CannotEmptyField, ExceptionMessageHelper.CannotEmptyField("Address Title"));
                #endregion

                var data = GetAllCachedData<CustomerAddressDto>().ToList();

                var entity = dto.ConvertTo<CustomerAdressEntitiy>();
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
                    entity = conn.Get<CustomerAdressEntitiy>(Id);
                }
                var result = entity.ConvertTo<CustomerAddressDto>();
                data.Add(result);
                FillCacheData(data);
                return result;
            }
            catch (KnownException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.AddLog(LogTypeEnum.Error, "CustomerManager.CreateOrUpdateCustomerAddress", RequestUserId, ex.Message, dto.ToJson(), ex);
                throw new KnownException(ErrorTypeEnum.UnexpectedExeption, ex.Message, ex);
            }
        }
        public CustomerAddressDto GetSingleCustomerAddress(int id, int RequestUserId, string TokenKey)
        {
            try
            {
                CheckAuthentication(RequestUserId, TokenKey);
                var data = GetAllCachedData<CustomerAddressDto>();
                var result = data.FirstOrDefault(q => q.Id == id);
                return result;
            }
            catch (KnownException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.AddLog(LogTypeEnum.Error, "CustomerManager.GetSingleCustomerAddress", RequestUserId, ex.Message, ex);
                throw new KnownException(ErrorTypeEnum.UnexpectedExeption, ex.Message, ex);
            }
        }
        public IEnumerable<CustomerAddressDto> GetAllCustomerAddress(int CustomerId, int RequestUserId, string TokenKey)
        {
            try
            {
                CheckAuthentication(RequestUserId, TokenKey);
                var data = GetAllCachedData<CustomerAddressDto>();
                var result = data.Where(q => q.CustomerId == CustomerId);
                return result;
            }
            catch (KnownException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.AddLog(LogTypeEnum.Error, "CustomerManager.GetAllCustomerAddress", RequestUserId, ex.Message, ex);
                throw new KnownException(ErrorTypeEnum.UnexpectedExeption, ex.Message, ex);
            }
        }
        #endregion

        private void CheckAuthentication(int RUserId, string TKey)
        {
            if (!Authentication.CheckTokenAuthentication(RUserId, TKey))
            {
                Logger.AddLog(LogTypeEnum.Warn, "CustomerManager.CheckAuthentication", RUserId, ExceptionMessageHelper.UnauthorizedAccess(RUserId), null);
                throw new KnownException(ErrorTypeEnum.AuthorizeException, ExceptionMessageHelper.UnauthorizedAccess(RUserId), string.Empty);
            }
        }
    }
}

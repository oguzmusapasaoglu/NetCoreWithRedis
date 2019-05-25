using Dapper.Contrib.Extensions;
using NetCoreWithRedis.Core.DbCore;
using NetCoreWithRedis.Core.Helper.CommonHelper;
using NetCoreWithRedis.Core.Helper.ExceptionHelper;
using NetCoreWithRedis.Core.Log.Services;
using NetCoreWithRedis.Domain.Authentication.Interface;
using NetCoreWithRedis.Domain.Users.Entity;
using NetCoreWithRedis.Domain.Users.Interface;
using NetCoreWithRedis.Shared.DTO;
using NetCoreWithRedis.Shared.DTO.Request;
using NetCoreWithRedis.Shared.DTO.Response;
using NetCoreWithRedis.Shared.DTO.Response.Base;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using NetCoreWithRedis.Core.CacheManager.Redis;
using NetCoreWithRedis.Core.Helper.QuerryManager;
using System.Data;
using NetCoreWithRedis.Shared.Entities;

namespace NetCoreWithRedis.Domain.Users.Manager
{
    public class UserManager : RedisManager, IUserManager
    {
        private ILogManager Logger;
        private IDbFactory Db;
        private IAuthenticationManager Authentication;

        public UserManager(
            ILogManager logger,
            IDbFactory db,
            IAuthenticationManager authentication)
        {
            Logger = logger;
            Db = db;
            Authentication = authentication;
        }
        public UsersDto CreateOrUpdate(UsersDto dto, int RequestUserId, string TokenKey)
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
                if (dto.EMail.IsNullOrEmpty())
                    throw new RequestWarningException(ErrorTypeEnum.WarningException, ExceptionCodeHelper.CannotEmptyField, ExceptionMessageHelper.CannotEmptyField("Email"));
                #endregion

                var data = GetAllCachedData<UsersDto>().ToList();

                #region Field Control
                if (data != null)
                {
                    if (data.Any(q => q.EMail == dto.EMail))
                        throw new RequestWarningException(ErrorTypeEnum.WarningException, ExceptionCodeHelper.IsInUse, ExceptionMessageHelper.IsInUse("Email"));
                    if (data.Any(q => q.UserName == dto.UserName))
                        throw new RequestWarningException(ErrorTypeEnum.WarningException, ExceptionCodeHelper.IsInUse, ExceptionMessageHelper.IsInUse("UserName"));
                }
                #endregion

                var entity = dto.ConvertTo<UserEntity>();
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
                    entity = conn.Get<UserEntity>(Id);
                }
                var result = entity.ConvertTo<UsersDto>();
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
                Logger.AddLog(LogTypeEnum.Error, "UserManager.CreateOrUpdate", RequestUserId, ex.Message, dto.ToJson(), ex);
                throw new KnownException(ErrorTypeEnum.UnexpectedExeption, ex.Message, ex);
            }
        }
        public PagingResultDto<IEnumerable<UsersDto>> GetAll(UserFilterRequestDto request, int RequestUserId, string TokenKey)
        {
            try
            {
                CheckAuthentication(RequestUserId, TokenKey);
                var data = GetAllCachedData<UsersDto>().AsQueryable();
                var predicate = PredicateBuilderHelper.True<UsersDto>();
                if (request.Id.HasValue)
                    predicate = predicate.And(q => q.Id == request.Id);
                if (request.ActivationStatus.HasValue)
                    predicate = predicate.And(q => q.ActivationStatus == request.ActivationStatus);
                if (!request.UserName.IsNullOrEmpty())
                    predicate = predicate.And(q => q.UserName.Contains(request.UserName));
                if (!request.FullName.IsNullOrEmpty())
                    predicate = predicate.And(q => q.FullName.Contains(request.FullName));
                if (!request.EMail.IsNullOrEmpty())
                    predicate = predicate.And(q => q.EMail == request.EMail);
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
                Logger.AddLog(LogTypeEnum.Error, "UserManager.GetAll", RequestUserId, ex.Message, request.ToJson(), ex);
                throw new KnownException(ErrorTypeEnum.UnexpectedExeption, ex.Message, ex);
            }
        }
        public UsersDto GetSingle(int id, int RequestUserId, string TokenKey)
        {
            try
            {
                CheckAuthentication(RequestUserId, TokenKey);
                var data = GetAllCachedData<UsersDto>();
                var result = data.FirstOrDefault(q => q.Id == id);
                return result;
            }
            catch (KnownException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.AddLog(LogTypeEnum.Error, "UserManager.GetSingle", RequestUserId, ex.Message, ex);
                throw new KnownException(ErrorTypeEnum.UnexpectedExeption, ex.Message, ex);
            }
        }
        public UsersDto GetSingle(UserLoginRequestDto request)
        {
            try
            {
                var ReturnData = new UserLoginResponseDto();
                var EncryptPass = PasswordHelper.EncryptData(request.Password);

                #region DynamicQuerryParameters
                var list = DynamicQuerryParameters.Create();
                list.Add(DynamicQuerryParameters.AddParameter("UserName", request.UserName, DbType.String));
                list.Add(DynamicQuerryParameters.AddParameter("Password", EncryptPass, DbType.String));
                list.Add(DynamicQuerryParameters.AddParameter("ActivationStatus", (int)ActivationStatusType.Active, DbType.Int16));
                #endregion

                var querry = QueryGenerator.GenerateQuery(list, QueryGenerator.tableName<UserEntity>());
                var result = Db.GetSingleData<UserEntity>(querry);
                if (result != null)
                {
                    ReturnData = result.ConvertTo<UserLoginResponseDto>();
                    ReturnData.TokenKey = Authentication.CreateTokenAuthentication(result.Id);
                }
                return ReturnData;
            }
            catch (KnownException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.AddLog(LogTypeEnum.Error, "UserManager.GetSingle", null, ex.Message, request.ToJson(), ex);
                throw new KnownException(ErrorTypeEnum.UnexpectedExeption, ex.Message, ex);
            }
        }

        private void CheckAuthentication(int RUserId, string TKey)
        {
            if (!Authentication.CheckTokenAuthentication(RUserId, TKey))
            {
                Logger.AddLog(LogTypeEnum.Warn, "UserManager.CheckAuthentication", RUserId, ExceptionMessageHelper.UnauthorizedAccess(RUserId), string.Empty);
                throw new KnownException(ErrorTypeEnum.AuthorizeException, ExceptionMessageHelper.UnauthorizedAccess(RUserId), string.Empty);
            }
        }
    }
}

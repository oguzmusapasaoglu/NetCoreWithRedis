using Dapper.Contrib.Extensions;
using NetCoreWithRedis.Core.CacheManager.Redis;
using NetCoreWithRedis.Core.DbCore;
using NetCoreWithRedis.Core.Helper.CommonHelper;
using NetCoreWithRedis.Core.Helper.ExceptionHelper;
using NetCoreWithRedis.Core.Log.Interface;
using NetCoreWithRedis.Domain.Users.Entity;
using NetCoreWithRedis.Domain.Users.Interface;
using NetCoreWithRedis.Shared.DTO;
using NetCoreWithRedis.Shared.DTO.Request;
using NetCoreWithRedis.Shared.DTO.Response;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreWithRedis.Domain.Users.Manager
{
    public class UserManager : IUserManager
    {
        private ILogService Logger;
        private IRedisManager CacheManager;
        private IDbFactory Db;
        public UserManager(ILogService logger, IRedisManager cache, IDbFactory db)
        {
            Logger = logger;
            CacheManager = cache;
            Db = db;
        }
        public UsersDto CreateOrUpdate(UsersDto dto, int RequestUserId)
        {
            try
            {
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

                var data = CacheManager.GetAll<UserEntity>().ToList();

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
                }

                int Id = conn.Insert(entity, Db._DbTransaction).ToInt();
                entity = conn.Get<UserEntity>(Id);
                data.Add(entity);
                CacheManager.FillCache(data);
                return entity.ConvertTo<UsersDto>();
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
        public IEnumerable<UserFilterResponseDto> GetAll(UserFilterRequestDto request, int RequestUserId)
        {
            try
            {
                var data = CacheManager.GetAll<UserEntity>().AsQueryable();
                var predicate = PredicateBuilderHelper.True<UserEntity>();
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
                var retundata = result.ConvertTo<IEnumerable<UserFilterResponseDto>>();
                retundata.Each(q => q.TotalRowCount = result.Count());
                retundata.Skip(request.RowsPerPage * request.PageNumber).Take(request.RowsPerPage);
                return retundata;
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
        public UsersDto GetSingle(int id, int RequestUserId)
        {
            try
            {
                var data = CacheManager.GetAll<UserEntity>();
                var result = data.FirstOrDefault(q => q.Id == id);
                return result.ConvertTo<UsersDto>();
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
        public UsersDto GetSingle(UserLoginRequestDto request, int RequestUserId)
        {
            try
            {
                var data = CacheManager.GetAll<UserEntity>();
                var EncryptPass = PasswordHelper.EncryptData(request.Password);
                var result = data.FirstOrDefault(q => q.UserName == request.UserName && q.Password == EncryptPass);
                return result.ConvertTo<UsersDto>();
            }
            catch (KnownException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.AddLog(LogTypeEnum.Error, "UserManager.GetSingle", RequestUserId, ex.Message, request.ToJson(), ex);
                throw new KnownException(ErrorTypeEnum.UnexpectedExeption, ex.Message, ex);
            }
        }
    }
}

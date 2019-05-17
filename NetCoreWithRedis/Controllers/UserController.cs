using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreWithRedis.Core.Helper.ExceptionHelper;
using NetCoreWithRedis.Core.Log.Interface;
using NetCoreWithRedis.Domain.Users.Interface;
using NetCoreWithRedis.Shared.DTO;
using NetCoreWithRedis.Shared.DTO.Request;
using NetCoreWithRedis.Shared.Entities;
using NetCoreWithRedis.Shared.Services;
using ServiceStack;

namespace NetCoreWithRedis.Controllers
{
    [ServiceStack.Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        ILogService Logger;
        IUserManager Manager;
        public UserController(IUserManager manager, ILogService logger)
        {
            Manager = manager;
            Logger = logger;
        }

        [HttpPost]
        public BackofficeResponse CreateOrUpdate(BackofficeRequest request)
        {
            long RequestDate = DateTimeHelper.Now;
            var response = new BackofficeResponse();
            try
            {
                var dto = request.RData.FromJson<UsersDto>();
                var result = Manager.CreateOrUpdate(dto, request.RUserId);
                response.Data = result.ToJson();
                response.ResponseStatus = ResponseStatusType.OK.ToBool();
            }
            catch (RequestWarningException rex)
            {
                response.ResponseStatus = ResponseStatusType.ERROR.ToBool();
                response.ErrorType = rex.ErrorTypeProp.ToString();
                response.ErrorMessage = rex.ExceptionMessage;
            }
            catch (KnownException kex)
            {
                response.ResponseStatus = ResponseStatusType.ERROR.ToBool();
                response.ErrorType = kex.ErrorTypeProp.ToString();
                response.ErrorMessage = kex.ExceptionMessage;
            }
            catch (Exception ex)
            {
                response.ResponseStatus = ResponseStatusType.ERROR.ToBool();
                response.ErrorType = ErrorTypeEnum.UnexpectedExeption.ToString();
                response.ErrorMessage = ex.StackTrace;
                Logger.AddFattalLog(LogTypeEnum.Fattal, "UserController.CreateOrUpdate", ex.Message, request.ToJson(), ex);
            }
            Logger.AddResponseLog((response.ResponseStatus == ResponseStatusType.OK.ToBool()) ?
                LogTypeEnum.Info :
                LogTypeEnum.Error, 
                "UserController.CreateOrUpdate", 
                request.RUserId, 
                response.ErrorMessage, 
                request.ToJson(), 
                RequestDate, 
                response.ToJson(), 
                DateTimeHelper.Now);
            return response;
        }

        [HttpPost]
        public BackofficeResponse GetSingle(BackofficeRequest request)
        {
            long RequestDate = DateTimeHelper.Now;
            var response = new BackofficeResponse();
            try
            {
                var dto = request.RData.FromJson<UsersDto>();
                var result = Manager.GetSingle(dto.Id, request.RUserId);
                response.Data = result.ToJson();
                response.ResponseStatus = ResponseStatusType.OK.ToBool();
            }
            catch (RequestWarningException rex)
            {
                response.ResponseStatus = ResponseStatusType.ERROR.ToBool();
                response.ErrorType = rex.ErrorTypeProp.ToString();
                response.ErrorMessage = rex.ExceptionMessage;
            }
            catch (KnownException kex)
            {
                response.ResponseStatus = ResponseStatusType.ERROR.ToBool();
                response.ErrorType = kex.ErrorTypeProp.ToString();
                response.ErrorMessage = kex.ExceptionMessage;
            }
            catch (Exception ex)
            {
                response.ResponseStatus = ResponseStatusType.ERROR.ToBool();
                response.ErrorType = ErrorTypeEnum.UnexpectedExeption.ToString();
                response.ErrorMessage = ex.StackTrace;
                Logger.AddFattalLog(LogTypeEnum.Fattal, "UserController.GetSingle", ex.Message, request.ToJson(), ex);
            }
            Logger.AddResponseLog((response.ResponseStatus == ResponseStatusType.OK.ToBool()) ?
                LogTypeEnum.Info :
                LogTypeEnum.Error,
                "UserController.GetSingle",
                request.RUserId,
                response.ErrorMessage,
                request.ToJson(),
                RequestDate,
                response.ToJson(),
                DateTimeHelper.Now);
            return response;
        }


        [HttpPost]
        public BackofficeResponse GetLoginInfo(BackofficeRequest request)
        {
            long RequestDate = DateTimeHelper.Now;
            var response = new BackofficeResponse();
            try
            {
                var dto = request.RData.FromJson<UserLoginRequestDto>();
                var result = Manager.GetSingle(dto, request.RUserId);
                response.Data = result.ToJson();
                response.ResponseStatus = ResponseStatusType.OK.ToBool();
            }
            catch (RequestWarningException rex)
            {
                response.ResponseStatus = ResponseStatusType.ERROR.ToBool();
                response.ErrorType = rex.ErrorTypeProp.ToString();
                response.ErrorMessage = rex.ExceptionMessage;
            }
            catch (KnownException kex)
            {
                response.ResponseStatus = ResponseStatusType.ERROR.ToBool();
                response.ErrorType = kex.ErrorTypeProp.ToString();
                response.ErrorMessage = kex.ExceptionMessage;
            }
            catch (Exception ex)
            {
                response.ResponseStatus = ResponseStatusType.ERROR.ToBool();
                response.ErrorType = ErrorTypeEnum.UnexpectedExeption.ToString();
                response.ErrorMessage = ex.StackTrace;
                Logger.AddFattalLog(LogTypeEnum.Fattal, "UserController.GetLoginInfo", ex.Message, request.ToJson(), ex);
            }
            Logger.AddResponseLog((response.ResponseStatus == ResponseStatusType.OK.ToBool()) ?
                LogTypeEnum.Info :
                LogTypeEnum.Error,
                "UserController.GetLoginInfo",
                request.RUserId,
                response.ErrorMessage,
                request.ToJson(),
                RequestDate,
                response.ToJson(),
                DateTimeHelper.Now);
            return response;
        }

        [HttpPost]
        public BackofficeResponse GetAll(BackofficeRequest request)
        {
            long RequestDate = DateTimeHelper.Now;
            var response = new BackofficeResponse();
            try
            {
                var dto = request.RData.FromJson<UserFilterRequestDto>();
                var result = Manager.GetAll(dto, request.RUserId);
                response.Data = result.ToJson();
                response.ResponseStatus = ResponseStatusType.OK.ToBool();
            }
            catch (RequestWarningException rex)
            {
                response.ResponseStatus = ResponseStatusType.ERROR.ToBool();
                response.ErrorType = rex.ErrorTypeProp.ToString();
                response.ErrorMessage = rex.ExceptionMessage;
            }
            catch (KnownException kex)
            {
                response.ResponseStatus = ResponseStatusType.ERROR.ToBool();
                response.ErrorType = kex.ErrorTypeProp.ToString();
                response.ErrorMessage = kex.ExceptionMessage;
            }
            catch (Exception ex)
            {
                response.ResponseStatus = ResponseStatusType.ERROR.ToBool();
                response.ErrorType = ErrorTypeEnum.UnexpectedExeption.ToString();
                response.ErrorMessage = ex.StackTrace;
                Logger.AddFattalLog(LogTypeEnum.Fattal, "UserController.GetAll", ex.Message, request.ToJson(), ex);
            }
            Logger.AddResponseLog((response.ResponseStatus == ResponseStatusType.OK.ToBool()) ?
                LogTypeEnum.Info :
                LogTypeEnum.Error,
                "UserController.GetAll",
                request.RUserId,
                response.ErrorMessage,
                request.ToJson(),
                RequestDate,
                response.ToJson(),
                DateTimeHelper.Now);
            return response;
        }
    }
}
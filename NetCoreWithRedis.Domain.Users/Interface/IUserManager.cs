using NetCoreWithRedis.Shared.DTO;
using NetCoreWithRedis.Shared.DTO.Request;
using NetCoreWithRedis.Shared.DTO.Response.Base;
using System.Collections.Generic;

namespace NetCoreWithRedis.Domain.Users.Interface
{
    public interface IUserManager
    {
        UsersDto CreateOrUpdate(UsersDto dto, int RequestUserId, string TokenKey);
        UsersDto GetSingle(int id, int RequestUserId, string TokenKey);
        UsersDto GetSingle(UserLoginRequestDto request);
        PagingResultDto<IEnumerable<UsersDto>> GetAll(UserFilterRequestDto request, int RequestUserId, string TokenKey);
    }
}

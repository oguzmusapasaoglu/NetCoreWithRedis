using NetCoreWithRedis.Shared.DTO;
using NetCoreWithRedis.Shared.DTO.Request;
using NetCoreWithRedis.Shared.DTO.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreWithRedis.Domain.Users.Interface
{
    public interface IUserManager
    {
        UsersDto CreateOrUpdate(UsersDto dto, int RequestUserId);
        UsersDto GetSingle(int id, int RequestUserId);
        UsersDto GetSingle(UserLoginRequestDto request, int RequestUserId);
        IEnumerable<UserFilterResponseDto> GetAll(UserFilterRequestDto request, int RequestUserId);
    }
}

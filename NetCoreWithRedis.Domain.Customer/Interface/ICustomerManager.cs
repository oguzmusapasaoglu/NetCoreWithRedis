using NetCoreWithRedis.Shared.DTO;
using NetCoreWithRedis.Shared.DTO.Request;
using NetCoreWithRedis.Shared.DTO.Response.Base;
using System.Collections.Generic;

namespace NetCoreWithRedis.Domain.Users.Interface
{
    public interface ICustomerManager
    {
        CustomerDto CreateOrUpdateCustomer(CustomerDto dto, int RequestUserId, string TokenKey);
        CustomerDto GetSingleCustomer(int id, int RequestUserId, string TokenKey);
        CustomerDto GetSingleCustomer(CustomerDto request);
        PagingResultDto<IEnumerable<CustomerDto>> GetAllCustomer(CustomerFilterRequestDto request, int RequestUserId, string TokenKey);

        CustomerAddressDto CreateOrUpdateCustomerAddress(CustomerAddressDto dto, int RequestUserId, string TokenKey);
        CustomerAddressDto GetSingleCustomerAddress(int id, int RequestUserId, string TokenKey);
        IEnumerable<CustomerAddressDto> GetAllCustomerAddress(int CustomerId, int RequestUserId, string TokenKey);
    }
}

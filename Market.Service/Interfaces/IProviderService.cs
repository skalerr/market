using Market.Domain.Entity;
using Market.Domain.Response;

namespace Market.Service.Interfaces;

public interface IProviderService
{
    public Task<BaseResponse<List<Provider>>> GetProviders();
}
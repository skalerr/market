using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Market.Domain.Enum;
using Market.Domain.Response;
using Market.Service.Interfaces;

namespace Market.Service.Implementations;

public class ProviderService : IProviderService
{
    private readonly IProviderRepository _providerRepository;

    public ProviderService(IProviderRepository providerRepository)
    {
        _providerRepository = providerRepository;
    }

    public async Task<BaseResponse<List<Provider>>> GetProviders()
    {
        var response = new BaseResponse<List<Provider>>();
        try
        {
             var provider = await _providerRepository.SelectList();
             if (provider == null)
             {
                 response.StatusCode = StatusCode.NotFound;
                 response.Descripton = "Provider not found.";
                 return response;
             }
             response.Data = provider;
             response.StatusCode = StatusCode.Ok;
             return response;
        }
        catch (Exception e)
        {
            return new BaseResponse<List<Provider>>()
            {
                Descripton = $"[ProviderService]: {e.Message}",
                StatusCode = StatusCode.InternalServerError,
            };
        }
    }
}
using Market.Domain.Entity;
using Market.Domain.Response;

namespace Market.Service.Interfaces;

public interface IOrderService
{
    public Task<BaseResponse<List<Order>>> GetOrders();
}
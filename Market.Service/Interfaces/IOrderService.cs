using Market.Domain.Entity;
using Market.Domain.Response;

namespace Market.Service.Interfaces;

public interface IOrderService
{
    public Task<BaseResponse<List<Order>>> GetOrders();
    public Task<BaseResponse<Order>> GetOrder(int id);
    public Task<BaseResponse<bool>> CreateOrder(Order order);
    public Task<BaseResponse<bool>> UpdateOrder(Order order);
    public Task<BaseResponse<bool>> Delete(Order order);
    
}
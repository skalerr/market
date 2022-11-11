using Market.Domain.Entity;
using Market.Domain.Response;
using Market.Domain.ViewModels.Order;

namespace Market.Service.Interfaces;

public interface IOrderService
{
    public Task<BaseResponse<List<Order>>> GetOrders();
    public Task<BaseResponse<Order>> GetOrder(int id);
    public Task<BaseResponse<bool>> CreateOrder(OrderViewModel model);
    public Task<BaseResponse<bool>> UpdateOrder(OrderViewModel model);
    public Task<BaseResponse<bool>> Delete(int id);
    
}
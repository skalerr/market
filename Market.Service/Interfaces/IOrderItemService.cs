using Market.Domain.Entity;
using Market.Domain.Response;

namespace Market.Service.Interfaces;

public interface IOrderItemService
{
    public Task<BaseResponse<OrderItem>> GetOrderItem(int id);
    public Task<BaseResponse<bool>> CreateOrder(OrderItem order);
    public Task<BaseResponse<bool>> UpdateOrder(OrderItem order);
    public Task<BaseResponse<bool>> Delete(OrderItem order);
    public Task<BaseResponse<OrderItem>> GetByName(string name);
    public Task<BaseResponse<List<OrderItem>>> GetOrderItemsByOrderId(int id);
}
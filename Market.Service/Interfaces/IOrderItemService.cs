using Market.Domain.Entity;
using Market.Domain.Response;
using Market.Domain.ViewModels.OrderItem;

namespace Market.Service.Interfaces;

public interface IOrderItemService
{
    public Task<BaseResponse<OrderItem>> GetOrderItem(int id);
    public Task<BaseResponse<bool>> CreateOrder(OrderItemViewModel model);
    public Task<BaseResponse<bool>> UpdateOrder(OrderItemViewModel model);
    public Task<BaseResponse<bool>> Delete(int id);
    public Task<BaseResponse<OrderItem>> GetByName(string name);
    public Task<BaseResponse<List<OrderItem>>> GetOrderItemsByOrderId(int id);
}
using Market.Domain.Entity;
using Market.Domain.Response;
using Market.Domain.ViewModels.OrderItem;

namespace Market.Service.Interfaces;

public interface IOrderItemService
{
    public Task<BaseResponse<OrderItem>> GetOrderItem(int id);
    public Task<BaseResponse<bool>> CreateOrderItem(OrderItemViewModel model);
    public Task<BaseResponse<bool>> UpdateOrderItem(OrderItemViewModel model);
    public Task<BaseResponse<bool>> DeleteOrderItem(int id);
    public Task<BaseResponse<OrderItem>> GetOrderItemByName(string name);
    public Task<BaseResponse<List<OrderItem>>> GetOrderItemsByOrderId(int id);
}
using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Market.Domain.Enum;
using Market.Domain.Response;
using Market.Domain.ViewModels.Order;
using Market.Domain.ViewModels.OrderItem;
using Market.Service.Interfaces;

namespace Market.Service.Implementations;

public class OrderItemService : IOrderItemService
{
    private readonly IOrderItemRepository _orderItemRepository;

    public OrderItemService(IOrderItemRepository orderItemRepository)
    {
        _orderItemRepository = orderItemRepository;
    }

    public async Task<BaseResponse<OrderItem>> GetOrderItem(int id)
    {
        var response = new BaseResponse<OrderItem>();
        try
        {
            response.Data = await _orderItemRepository.Get(id);
        }
        catch (Exception e)
        {
            return new BaseResponse<OrderItem>()
            {
                Descripton = $"[OrderItemService]: {e.Message}",
                StatusCode = StatusCode.NotFound,
            };
        }
        response.StatusCode = StatusCode.Ok;
        return response;
    }

    public async Task<BaseResponse<bool>> CreateOrderItem(OrderItemViewModel model)
    {
        var response = new BaseResponse<bool>();
        try
        
        {
            var orderItem = new OrderItem()
            {
                Name = model.Name,
                Quantity = model.Quantity,
                Unit = model.Unit,
                OrderId = model.OrderId,
            };

            var orderNumber = await _orderItemRepository.StringOrderNumber(orderItem);
            if (model.Name == orderNumber)
            {
                return new BaseResponse<bool>()
                {
                    Descripton = $"[OrderService]: Duplicate Name and Number",
                    StatusCode = StatusCode.DuplicateName,
                    Data = false,
                };
            }

            response.Data = await _orderItemRepository.Create(orderItem);
        }
        catch (Exception e)
        {
            return new BaseResponse<bool>()
            {
                Descripton = $"[OrderItemService]: {e.Message}",
                StatusCode = StatusCode.InternalServerError,
            };
        }

        response.StatusCode = StatusCode.Ok;
        return response;
    }

    public async Task<BaseResponse<bool>> UpdateOrderItem(OrderItemViewModel model)
    {
        var response = new BaseResponse<bool>();
        try
        {
            var orderItem = new OrderItem()
            {
                Id = model.Id,
                Name = model.Name,
                Quantity = model.Quantity,
                Unit = model.Unit,
                OrderId = model.OrderId,
                
            };
            var orderNumber = await _orderItemRepository.StringOrderNumber(orderItem);
            if (model.Name == orderNumber)
            {
                return new BaseResponse<bool>()
                {
                    Descripton = $"[OrderService]: Duplicate Name and Number",
                    StatusCode = StatusCode.DuplicateName,
                    Data = false,
                };
            }
            response.Data = await _orderItemRepository.Update(orderItem);
        }
        catch (Exception e)
        {
            return new BaseResponse<bool>()
            {
                Descripton = $"[OrderItemService]: {e.Message}",
                StatusCode = StatusCode.InternalServerError,
            };
        }
        response.StatusCode = StatusCode.Ok;
        return response;
    }

    public async Task<BaseResponse<bool>> DeleteOrderItem(int id)
    {
        var response = new BaseResponse<bool>();
        try
        {
            var orderItem = await _orderItemRepository.Get(id);
            if (orderItem == null)
            {
                response.Descripton = "Not found.";
                response.StatusCode = StatusCode.OrderItemNotFound;
                response.Data = false;
                return response;
            }
            response.Data = await _orderItemRepository.Delete(orderItem);
        }
        catch (Exception e)
        {
            return new BaseResponse<bool>()
            {
                Descripton = $"[OrderItemService]: {e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
        response.StatusCode = StatusCode.Ok;
        return response;
    }

    public async Task<BaseResponse<OrderItem>> GetOrderItemByName(string name)
    {
        var response = new BaseResponse<OrderItem>();
        try
        {
            response.Data = await _orderItemRepository.GetByName(name);
            if (response.Data == null)
            {
                response.Descripton = "Items not found.";
                response.StatusCode = StatusCode.OrderItemNotFound;
                return response;
            }
        }
        catch (Exception e)
        {
            return new BaseResponse<OrderItem>()
            {
                Descripton = $"[OrderItemService]: {e.Message}",
                StatusCode = StatusCode.NotFound
            };
        }

        response.StatusCode = StatusCode.Ok;
        return response;
    }

    public async Task<BaseResponse<List<OrderItem>>> GetOrderItemsByOrderId(int id)
    {
        var response = new BaseResponse<List<OrderItem>>();
        try
        {
            response.Data = await _orderItemRepository.GetAllByOrderId(id);
            if (response.Data == null)
            {
                response.Descripton = "Items not found.";
                response.StatusCode = StatusCode.OrderItemNotFound;
                return response;
            }
        }
        catch (Exception e)
        {
            return new BaseResponse<List<OrderItem>>()
            {
                Descripton = $"[OrderItemService]: {e.Message}",
                StatusCode = StatusCode.NotFound,
            };
        }

        response.StatusCode = StatusCode.Ok;
        return response;
    }
}

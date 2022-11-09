using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Market.Domain.Enum;
using Market.Domain.Response;
using Market.Service.Interfaces;

namespace Market.Service.Implementations;

public class OrderItemService : IOrderItemService
{
    private readonly IOrderItemRepository _orderRepository;

    public OrderItemService(IOrderItemRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }


    public async Task<BaseResponse<OrderItem>> GetOrderItem(int id)
    {
        var response = new BaseResponse<OrderItem>();
        try
        {
            response.Data = await _orderRepository.Get(id);
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

    public async Task<BaseResponse<bool>> CreateOrder(OrderItem order)
    {
        var response = new BaseResponse<bool>();
        try
        {
            response.Data = await _orderRepository.Create(order);
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

    public async Task<BaseResponse<bool>> UpdateOrder(OrderItem order)
    {
        var response = new BaseResponse<bool>();
        try
        {
            response.Data = await _orderRepository.Update(order);
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

    public async Task<BaseResponse<bool>> Delete(OrderItem order)
    {
        var response = new BaseResponse<bool>();
        try
        {
            response.Data = await _orderRepository.Delete(order);
            
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

    public async Task<BaseResponse<OrderItem>> GetByName(string name)
    {
        var response = new BaseResponse<OrderItem>();
        try
        {
            response.Data = await _orderRepository.GetByName(name);
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
            response.Data = await _orderRepository.GetAllByOrderId(id);
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

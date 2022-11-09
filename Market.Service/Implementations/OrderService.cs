using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Market.Domain.Enum;
using Market.Domain.Response;
using Market.Service.Interfaces;

namespace Market.Service.Implementations;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<BaseResponse<List<Order>>> GetOrders()
    {
        var baseresp = new BaseResponse<List<Order>>();
        try
        {
            var orders= await _orderRepository.SelectList();
            if (orders.Count == 0)
            {
                baseresp.StatusCode = StatusCode.NotFound;
                baseresp.Descripton = $"Найдено 0 элементов";
                return baseresp;
            }

            baseresp.Data = orders;
            baseresp.StatusCode = StatusCode.Ok;
            return baseresp;
        }
        catch (Exception e)
        {
            return new BaseResponse<List<Order>>()
            {
                Descripton = $"[OrderService]: {e.Message}",
                StatusCode = StatusCode.InternalServerError,
            };
        }
    }

    public async Task<BaseResponse<Order>> GetOrder(int id)
    {
        var baseresp = new BaseResponse<Order>();
        try
        {
            baseresp.Data = await _orderRepository.Get(id);
        }
        catch (Exception e)
        {
            return new BaseResponse<Order>()
            {
                Descripton = $"[OrderService]: {e.Message}",
                StatusCode = StatusCode.InternalServerError,
            };
        }
        baseresp.StatusCode = StatusCode.Ok;
        return baseresp;
    }

    public async Task<BaseResponse<bool>> CreateOrder(Order order)
    {
        var baseresp = new BaseResponse<bool>();
        try
        {
            baseresp.Data = await _orderRepository.Create(order);
        }
        catch (Exception e)
        {
            return new BaseResponse<bool>()
            {
                Descripton = $"[OrderService]: {e.Message}",
                StatusCode = StatusCode.InternalServerError,
            };
        }
        baseresp.StatusCode = StatusCode.Ok;
        return baseresp;
    }

    public async Task<BaseResponse<bool>> UpdateOrder(Order order)
    {
        var baseresp = new BaseResponse<bool>();
        try
        {
            baseresp.Data = await _orderRepository.Update(order);
        }
        catch (Exception e)
        {
            return new BaseResponse<bool>()
            {
                Descripton = $"[OrderService]: {e.Message}",
                StatusCode = StatusCode.InternalServerError,
            };
        }
        baseresp.StatusCode = StatusCode.Ok;
        return baseresp;
    }

    public async Task<BaseResponse<bool>> Delete(Order order)
    {
        var baseresp = new BaseResponse<bool>();
        try
        {
            baseresp.Data = await _orderRepository.Delete(order);
        }
        catch (Exception e)
        {
            return new BaseResponse<bool>()
            {
                Descripton = $"[OrderService]: {e.Message}",
                StatusCode = StatusCode.InternalServerError,
            };
        }
        baseresp.StatusCode = StatusCode.Ok;
        return baseresp;
    }
}
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
}
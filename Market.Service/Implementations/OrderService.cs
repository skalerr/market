using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Market.Domain.Enum;
using Market.Domain.Response;
using Market.Domain.ViewModels.Order;
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
        var resp = new BaseResponse<List<Order>>();
        try
        {
            var orders= await _orderRepository.SelectList();
            if (orders.Count == 0)
            {
                resp.StatusCode = StatusCode.OrderNotFound;
                resp.Descripton = $"Найдено 0 элементов";
                return resp;
            }

            resp.Data = orders;
            resp.StatusCode = StatusCode.Ok;
            return resp;
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
        var resp = new BaseResponse<Order>();
        try
        {
            resp.Data = await _orderRepository.Get(id);
        }
        catch (Exception e)
        {
            return new BaseResponse<Order>()
            {
                Descripton = $"[OrderService]: {e.Message}",
                StatusCode = StatusCode.InternalServerError,
            };
        }
        resp.StatusCode = StatusCode.Ok;
        return resp;
    }

    public async Task<BaseResponse<bool>> CreateOrder(OrderViewModel model)
    {
        var resp = new BaseResponse<bool>();
        try
        {
            var order = new Order()
            {
                Date = model.Date,
                Number = model.Number,
                Provider = model.Provider,
                ProviderId = model.ProviderId
            };
            
            // var number = await _orderRepository.NotUniqueNumber(order);
            var provider = await _orderRepository.NotUniqueProvide(order);
            if (provider)
            {
                return new BaseResponse<bool>()
                {
                    Descripton = $"[OrderService]: Duplicate name and Provider",
                    StatusCode = StatusCode.DuplicateName,
                    Data = false,
                };
            }
            
            resp.Data = await _orderRepository.Create(order);
        }
        catch (Exception e)
        {
            return new BaseResponse<bool>()
            {
                Descripton = $"[OrderService]: {e.Message}",
                StatusCode = StatusCode.InternalServerError,
                Data = false,
            };
        }
        resp.StatusCode = StatusCode.Ok;
        return resp;
    }

    public async Task<BaseResponse<bool>> UpdateOrder(OrderViewModel model)
    {
        var resp = new BaseResponse<bool>();
        try
        {
            var order = new Order()
            {
                Id = model.Id,
                Date = model.Date,
                Number = model.Number,
                ProviderId = model.ProviderId,
            };
            // var number = await _orderRepository.NotUniqueNumber(order);
            var isUniqueProvide = await _orderRepository.NotUniqueProvide(order);
            var databaseOrder = await _orderRepository.Get(order.Id);

            // Здесь проверка на уникальность состоит в том что если передают тот же номер и айди провайдера,
            // то выбьет ошибку уникальности, для этого во втором ифе есть проверка на совпадение айдишника или имени.
            if (isUniqueProvide)
            {
                if (model.Number != databaseOrder.Number || model.ProviderId != databaseOrder.ProviderId)
                {
                    return new BaseResponse<bool>()
                    {
                        Descripton = $"[OrderService]: Duplicate name and Provider",
                        StatusCode = StatusCode.DuplicateName,
                        Data = false,
                    };
                }
            }
            resp.Data = await _orderRepository.Update(order);
        }
        catch (Exception e)
        {
            return new BaseResponse<bool>()
            {
                Descripton = $"[OrderService]: {e.Message}",
                StatusCode = StatusCode.InternalServerError,
            };
        }
        resp.StatusCode = StatusCode.Ok;
        return resp;
    }

    public async Task<BaseResponse<bool>> Delete(int id)
    {
        var resp = new BaseResponse<bool>();
        try
        {
            var order = await _orderRepository.Get(id);
            if (order == null)
            {
                resp.Descripton = "Not found.";
                resp.StatusCode = StatusCode.OrderNotFound;
                resp.Data = false;
                return resp;
            }
            resp.Data = await _orderRepository.Delete(order);
        }
        catch (Exception e)
        {
            return new BaseResponse<bool>()
            {
                Descripton = $"[OrderService]: {e.Message}",
                StatusCode = StatusCode.InternalServerError,
            };
        }
        resp.StatusCode = StatusCode.Ok;
        return resp;
    }
    
    // public async Task<BaseResponse<Order>> GetOrderByItemId(int id)
    // {
    //     var resp = new BaseResponse<Order>();
    //     try
    //     {
    //         resp.Data = await _orderRepository.Get(id);
    //     }
    //     catch (Exception e)
    //     {
    //         return new BaseResponse<Order>()
    //         {
    //             Descripton = $"[OrderService]: {e.Message}",
    //             StatusCode = StatusCode.InternalServerError,
    //         };
    //     }
    //     resp.StatusCode = StatusCode.Ok;
    //     return resp;
    // }
    
    public async Task<BaseResponse<List<Order>>> GetOrderByDate(DateTime from, DateTime to)
    {
        var resp = new BaseResponse<List<Order>>();
        try
        {
            resp.Data = await _orderRepository.FilterByDate(from, to);
            if (!resp.Data.Any())
            {
                resp.Descripton = $"[OrderService]: Найдено 0 элементов";
                resp.StatusCode = StatusCode.NotFound;
                return resp;
            }
        }
        catch (Exception e)
        {
            return new BaseResponse<List<Order>>()
            {
                Descripton = $"[OrderService]: {e.Message}",
                StatusCode = StatusCode.InternalServerError,
            };
        }
        resp.StatusCode = StatusCode.Ok;
        return resp;
    }
}
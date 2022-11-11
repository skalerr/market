﻿using System.Diagnostics;
using Market.Domain.ViewModels.Order;
using Market.Domain.ViewModels.OrderItem;
using Microsoft.AspNetCore.Mvc;
using Market.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Market.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IOrderService _orderService;
    private readonly IOrderItemService _orderItemService;
    private readonly IProviderService _providerService;

    public HomeController(ILogger<HomeController> logger, IOrderService orderService, IOrderItemService orderItemService, ApplicationDbContext db, IProviderService providerService)
    {
        _logger = logger;
        _orderService = orderService;
        _orderItemService = orderItemService;
        _providerService = providerService;
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
    
    public async Task<IActionResult> Index()
    {
        var orders = await _orderService.GetOrders();
        ViewBag.Orders  = orders.Data;
        return View();
    }
    
    #region -- заказы --

    [HttpGet]
    public IActionResult CreateOrder()
    {
        var providers =  _providerService.GetProviders();
        ViewBag.Providers = providers.Result.Data;
        
        var order = new OrderViewModel()
        {
            Date = DateTime.Now.Date,
        };
        return View(order);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateOrder(OrderViewModel model)
    {
        var providers =  _providerService.GetProviders();
        ViewBag.Providers = providers.Result.Data;
        
        var resp= await _orderService.CreateOrder(model);
        if (resp.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            return RedirectToAction("Index");
        }
        return View(model);

    }
    
    public async Task<IActionResult> EditOrder(int id)
    {
        var providers =  await _providerService.GetProviders();
        ViewBag.Providers = providers.Data;
        
        var order = await _orderService.GetOrder(id);
        var model = new OrderViewModel()
        {
            Date = order.Data.Date,
            Id = order.Data.Id,
            Number = order.Data.Number,
            ProviderId = order.Data.ProviderId,
        };
        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> EditOrder(OrderViewModel model)
    {
        var providers = await _providerService.GetProviders();
        ViewBag.Providers = providers.Data;
        
        var resp = await _orderService.UpdateOrder(model);
        if (resp.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            return RedirectToAction("OrderInfo", new { model.Id});
        }
        return View(model);
    }
    
    public async Task<IActionResult> OrderInfo(int id)
    {
        var providers =  await _providerService.GetProviders();
        ViewBag.Providers = providers.Data;
        
        var order = await _orderService.GetOrder(id);
        var orderItems = await _orderItemService.GetOrderItemsByOrderId(id);
        if (orderItems.StatusCode != Domain.Enum.StatusCode.Ok)
        {
            return RedirectToAction("Index");
        }
        var orderModel = new OrderViewModel()
        {
            Date = order.Data.Date,
            Id = order.Data.Id,
            Number = order.Data.Number,
            ListItems = orderItems.Data,
            ProviderId = order.Data.ProviderId
        };

        if (order.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            return View(orderModel);
        }
        return RedirectToAction("Index");
    }
    
    public async Task<IActionResult> AddItemToOrder(OrderViewModel model)
    {
        var orderResponse = await _orderService.GetOrder(model.Id);

        if (orderResponse.StatusCode != Domain.Enum.StatusCode.Ok)
        {
            RedirectToAction("Index");
        }
        
        var data = new OrderItemViewModel()
        {
            Name = model.OrderItem.Name,
            Quantity = model.OrderItem.Quantity,
            Unit = model.OrderItem.Unit,
            OrderId = model.Id,
            
        };
        var itemResponse = await _orderItemService.CreateOrderItem(data);
        
        
        return itemResponse.StatusCode == Domain.Enum.StatusCode.Ok 
            ? RedirectToAction("OrderInfo", new { model.Id}) 
            : RedirectToAction("Index");
    }

    public async Task<IActionResult> DeleteOrder(int id)
    {
        var resp = await _orderService.Delete(id);
        return resp.StatusCode == Domain.Enum.StatusCode.Ok 
            ? RedirectToAction("Index") 
            : RedirectToAction("OrderInfo", new { id});
    }

    #endregion
    
    public async Task<IActionResult> Providers()
    {
        var model = await _providerService.GetProviders();
        return View(model.Data);
    }
}
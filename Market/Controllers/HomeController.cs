using System.Diagnostics;
using Market.Domain.ViewModels.Order;
using Microsoft.AspNetCore.Mvc;
using Market.Models;

namespace Market.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IOrderService _orderService;
    private readonly IOrderItemService _orderItemService;
    private readonly ApplicationDbContext _db;

    public HomeController(ILogger<HomeController> logger, IOrderService orderService, IOrderItemService orderItemService, ApplicationDbContext db)
    {
        _logger = logger;
        _orderService = orderService;
        _orderItemService = orderItemService;
        _db = db;
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }

    public IActionResult Index()
    {
        var resp = _orderService.GetOrders();
        ViewBag.Orders  = resp.Result.Data;
        return View();
    }

    [HttpGet]
    public IActionResult CreateOrder()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult CreateOrder(OrderViewModel model)
    {
        var resp= _orderService.CreateOrder(model);
        if (resp.Result.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            return RedirectToAction("Index");
        }
        return View(model);

    }
    
    public IActionResult EditOrder(int id)
    {
        var order = _orderService.GetOrder(id).Result.Data;
        return View(order);
    }
    
    [HttpPost]
    public IActionResult EditOrder(OrderViewModel model)
    {
        var resp = _orderService.UpdateOrder(model);
        if (resp.Result.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            RedirectToAction("Index");
        }
        return View(model);
    }

    public IActionResult Providers()
    {
        var model = _db.Providers.ToList();
        return View(model);
    }

    public IActionResult OrderInfo(int id)
    {
        var resp = _orderService.GetOrder(id);
        if (resp.Result.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            return View(resp.Result.Data);
        }
        return RedirectToAction("Index");
    }

    public IActionResult DeleteOrder(int id)
    {
        var resp = _orderService.Delete(id);
        return resp.Result.StatusCode == Domain.Enum.StatusCode.Ok 
            ? RedirectToAction("Index") 
            : RedirectToAction("OrderInfo", new { id});
    }
}
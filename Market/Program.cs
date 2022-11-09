global using Market.DAL;
global using Market.DAL.Interfaces;
global using Market.Domain.Entity;
global using Market.Service.Implementations;
global using Market.Service.Interfaces;
global using Microsoft.EntityFrameworkCore;
using Market.Domain.Interfaces;
using Market.Domain.Response;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var con = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(con));
//взаимодействие с бд

builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IOrderItemRepository, OrderItemRepository>();

//сервис для взаимодействия с заказами
builder.Services.AddTransient<IOrderService, OrderService>();

builder.Services.AddTransient<IOrderItemService, OrderItemService>(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
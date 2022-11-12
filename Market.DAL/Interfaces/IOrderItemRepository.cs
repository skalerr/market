using Market.Domain.Entity;

namespace Market.DAL.Interfaces;

public interface IOrderItemRepository : IBaseRepository<OrderItem>
{
    Task<OrderItem> GetByName(string name);
    Task<List<OrderItem>> GetAllByOrderId(int id);
    Task<bool> NotUniqueName(OrderItem entity);
    Task<bool> NotUniqueNumber(OrderItem entity);
    Task<string> StringOrderNumber(OrderItem entity);
}
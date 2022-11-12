using Market.Domain.Entity;

namespace Market.DAL.Interfaces;

public interface IOrderRepository : IBaseRepository<Order>
{
    Task<bool> NotUniqueNumber(Order entity);
    Task<bool> NotUniqueProvide(Order entity);
    Task<List<Order>> FilterByDate(DateTime from, DateTime to);
    
}
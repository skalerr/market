using Market.Domain.Entity;

namespace Market.DAL.Interfaces;

public class IOrderRepository : IBaseRepository<Order>
{
    private readonly ApplicationDbContext _db;

    public IOrderRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public bool Create(Order entity)
    {
        throw new NotImplementedException();
    }

    public Order Get(int id)
    {
        throw new NotImplementedException();
    }

    public List<Order> Select()
    {
        _db.Orders.ToList();
    }

    public bool Delete(Order entity)
    {
        throw new NotImplementedException();
    }

    public Order Update(Order entity)
    {
        throw new NotImplementedException();
    }
}
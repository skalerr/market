using Market.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Market.DAL.Interfaces;

public class OrderItemRepository : IOrderItemRepository
{

    private readonly ApplicationDbContext _db;

    public OrderItemRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Create(OrderItem entity)
    {
        try
        {
            await _db.OrderItems.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<OrderItem> Get(int id)
    {
        return await _db.OrderItems.FirstOrDefaultAsync(i => i.Id == id);
    }

    public Task<List<OrderItem>> SelectList()
    {
        return _db.OrderItems.ToListAsync();
    }

    public async Task<bool> Delete(OrderItem entity)
    {
        try
        {
            _db.OrderItems.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> Update(OrderItem entity)
    {
        try
        {
            _db.OrderItems.Update(entity);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<OrderItem> GetByName(string name)
    {
        return await _db.OrderItems.FirstOrDefaultAsync(i => i.Name == name);
    }

    public async Task<List<OrderItem>> GetAllByOrderId(int id)
    {
        var resp = await _db.OrderItems.Select(i => i).Where(i => i.OrderId == id).ToListAsync();
        return resp;
    }

    public async Task<bool> NotUniqueName(OrderItem entity)
    {
        var uniqueName = await _db.OrderItems.Where(o => o.Name == entity.Name).ToListAsync();
        return uniqueName.Count > 0;
    }

    public async Task<bool> NotUniqueNumber(OrderItem entity)
    {
        var uniqueName = await _db.OrderItems.Where(o => o.OrderId == entity.OrderId).ToListAsync();
        return uniqueName.Count > 0;
    }
    
    public async Task<string> StringOrderNumber(OrderItem entity)
    {
        var number = await _db.Orders.FindAsync(entity.OrderId);
        return number.Number.ToString();
    }
}
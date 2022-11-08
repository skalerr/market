﻿using Market.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Exception = System.Exception;

namespace Market.DAL.Interfaces;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _db;

    public OrderRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public async Task<bool> Create(Order entity)
    {
        try
        {
            await _db.Orders.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<Order> Get(int id)
    {
        return await _db.Orders.FirstOrDefaultAsync(i => i.Id == id);
    }

    public Task<List<Order>> Select()
    {
        return _db.Orders.ToListAsync();
    }

    public async Task<bool> Delete(Order entity)
    {
        try
        {
            _db.Orders.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> Update(Order entity)
    {
        try
        {
            _db.Orders.Update(entity);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
        
    }
}
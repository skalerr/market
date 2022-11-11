using Market.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Market.DAL.Interfaces;

public class ProviderRepository : IProviderRepository
{
    private readonly ApplicationDbContext _db;

    public ProviderRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public Task<bool> Create(Provider entity)
    {
        throw new NotImplementedException();
    }

    public Task<Provider> Get(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Provider>> SelectList()
    {
        return _db.Providers.ToList();
    }

    public Task<bool> Delete(Provider entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(Provider entity)
    {
        throw new NotImplementedException();
    }
}
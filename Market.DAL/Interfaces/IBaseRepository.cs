using Market.Domain.Entity;

namespace Market.DAL.Interfaces;

public interface IBaseRepository<T>
{
    Task<bool> Create(T entity);
    Task<T> Get(int id);
    Task<List<T>> SelectList();
    Task<bool> Delete(T entity);
    Task<bool> Update(T entity);

}
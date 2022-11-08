namespace Market.DAL.Interfaces;

public interface IBaseRepository<T>
{
    bool Create(T entity);
    T Get(int id);
    List<T> Select();
    bool Delete(T entity);
    T Update(T entity);

}
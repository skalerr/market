namespace Market.Domain.Interfaces;

public interface IBaseResponse<T>
{
    T Data { get; }
}
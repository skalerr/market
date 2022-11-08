using Market.Domain.Enum;
using Market.Domain.Interfaces;

namespace Market.Domain.Response;

public class BaseResponse<T> : IBaseResponse<T>
{
    public string Descripton { get; set; }
    public StatusCode StatusCode { get; set; }
    public T Data { get; set; }  
}


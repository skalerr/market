namespace Market.Domain.Enum;

public enum StatusCode
{
    OrderNotFound = 1,
    OrderItemNotFound = 2,
    DuplicateName = 3,
    
    
    Ok = 200,
    NotFound = 404,
    InternalServerError = 500,
}
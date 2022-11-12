using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Domain.Entity;

public class OrderItem
{
    public int Id { get; set; }
    
    [DisplayName("Номер заказа")]
    public int OrderId { get; set; }
    
    [ForeignKey("OrderId")]
    public Order Order { get; set; }
    
    [DisplayName("Название")]
    public string Name { get; set; }
    
    [DisplayName("Количество")]
    public decimal Quantity { get; set; }
    
    [DisplayName("Шт.")]
    public string Unit { get; set; }
}
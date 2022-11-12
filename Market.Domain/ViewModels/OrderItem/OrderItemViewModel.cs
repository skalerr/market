using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Domain.ViewModels.OrderItem;

public class OrderItemViewModel : Entity.OrderItem
{
    [DisplayName("Номер заказа")]
    public int OrderId { get; set; }
    
    [ForeignKey("OrderId")]
    public Entity.Order Order { get; set; }
    
    [DisplayName("Название")]
    public string Name { get; set; }
    
    [DisplayName("Количество")]
    public decimal Quantity { get; set; }
    
    [DisplayName("Шт.")]
    public string Unit { get; set; }
}
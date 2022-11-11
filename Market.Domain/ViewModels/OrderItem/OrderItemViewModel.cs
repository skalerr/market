using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Domain.ViewModels.OrderItem;

public class OrderItemViewModel
{
    public int OrderId { get; set; }
    
    [ForeignKey("OrderId")]
    public Entity.Order Order { get; set; }
    
    public string Name { get; set; }
    
    public decimal Quantity { get; set; }
    
    public string Unit { get; set; }
}
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Domain.Entity;

public class OrderItem
{
    public int Id { get; set; }
    
    public int OrderId { get; set; }
    
    [ForeignKey("OrderId")]
    public Order Order { get; set; }
    
    public string Name { get; set; }
    
    public decimal Quantity { get; set; }
    
    public string Unit { get; set; }
}
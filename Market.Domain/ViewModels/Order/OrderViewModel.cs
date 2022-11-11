using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Market.Domain.Entity;
using Market.Domain.ViewModels.OrderItem;

namespace Market.Domain.ViewModels.Order;

public class OrderViewModel 
{
    public int Id { get; set; }
    
    [Required]
    public int Number { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public int ProviderId { get; set; }
    
    [ForeignKey("ProviderId")]
    public Provider Provider { get; set; }

    public List<Entity.OrderItem> ListItems { get; set; }

    public Entity.OrderItem OrderItem { get; set; }
}
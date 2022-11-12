using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Market.Domain.Entity;
using Market.Domain.ViewModels.OrderItem;

namespace Market.Domain.ViewModels.Order;

public class OrderViewModel : Entity.Order
{
    [Required]
    [DisplayName("Номер")]
    public int Number { get; set; }
    
    [Required]
    [DisplayName("Дата")]
    public DateTime Date { get; set; }
    
    [Required]
    [DisplayName("Провайдер")]
    public int ProviderId { get; set; }
    
    [ForeignKey("ProviderId")]
    public Provider Provider { get; set; }

    public List<Entity.OrderItem> ListItems { get; set; }

    public Entity.OrderItem OrderItem { get; set; }
}
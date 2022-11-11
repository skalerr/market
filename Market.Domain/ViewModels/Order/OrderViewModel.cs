using System.ComponentModel.DataAnnotations.Schema;
using Market.Domain.Entity;

namespace Market.Domain.ViewModels.Order;

public class OrderViewModel : Entity.Order
{
    public int Number { get; set; }
    
    public DateTime Date { get; set; }
    
    public int ProviderId { get; set; }
    
    [ForeignKey("ProviderId")]
    public Provider Provider { get; set; }
}
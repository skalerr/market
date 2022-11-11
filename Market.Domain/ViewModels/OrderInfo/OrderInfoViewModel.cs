using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Market.Domain.Entity;

namespace Market.Domain.ViewModels.OrderInfo;

public class OrderInfoViewModel
{
    public class OrderViewModel : Entity.Order
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
    }
}
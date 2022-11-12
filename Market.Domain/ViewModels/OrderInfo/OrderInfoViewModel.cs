using System.ComponentModel;
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
    }
}
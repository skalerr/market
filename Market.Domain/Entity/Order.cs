using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Domain.Entity;

public class Order
{
    public int Id { get; set; }
    
    [DisplayName("Номер")]
    public int Number { get; set; }
    
    [DisplayName("Дата")]
    public DateTime Date { get; set; }
    
    [DisplayName("Провайдер")]
    public int ProviderId { get; set; }
    
    [ForeignKey("ProviderId")]
    public Provider Provider { get; set; }
}
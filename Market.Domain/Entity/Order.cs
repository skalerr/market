using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Domain.Entity;

public class Order
{
    public int Id { get; set; }
    public int Number { get; set; }
    public DateTime Date { get; set; }
    
    public int ProviderId { get; set; }
    [ForeignKey("ProviderId")]
    public Provider Provider { get; set; }
}
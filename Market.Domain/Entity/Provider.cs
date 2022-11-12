using System.ComponentModel;
using System.Globalization;

namespace Market.Domain.Entity;

public class Provider
{
    public int Id { get; set; }
    
    [DisplayName("Имя")]
    public string Name { get; set; }
}
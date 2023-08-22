using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Domain.Base;

namespace Domain.App;

public class InvoiceRow : DomainEntityId
{

    public Guid InvoiceId { get; set; }
    
    public Invoice? Invoice { get; set; }

    public Guid ServiceId { get; set; }
    public Service? Service { get; set; }

    public int Quantity { get; set; } = 1;
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal? PriceOverride { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Tax { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Total { get; set; }
    
}
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.Base;

namespace Domain.App;

public class PaymentMethod : DomainEntityId
{
    [MaxLength(200)]
    public string PaymentMethodName { get; set; } = default!;
    public string? MethodImageUrl { get; set; }
    
    public ICollection<Invoice>? Invoices { get; set; }
    
}
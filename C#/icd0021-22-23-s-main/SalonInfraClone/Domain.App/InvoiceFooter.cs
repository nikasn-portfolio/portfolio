using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.Base;

namespace Domain.App;

public class InvoiceFooter : DomainEntityId
{
    [MaxLength(200)]
    public string? Email { get; set; }
    [MaxLength(50)]
    public string? Phone { get; set; }
    [MaxLength(200)]
    public string Address { get; set; } = default!;
    [MaxLength(100)]
    public string? Iban { get; set; } = default!;
    [MaxLength(200)]
    public string CompanyName { get; set; } = default!;
    
    public ICollection<Invoice>? Invoices { get; set; }
}
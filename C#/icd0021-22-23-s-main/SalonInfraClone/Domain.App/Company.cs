using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.Base;

namespace Domain.App;

public class Company : DomainEntityId
{
    [MaxLength(200)]
    public string CompanyName { get; set; } = default!;
    [MaxLength(200)]
    public string? IdentificationCode { get; set; }
    [MaxLength(200)]
    public string? Address { get; set; }
    [MaxLength(200)]
    public string? VatNumber { get; set; }
    
    public ICollection<Invoice>? Invoices { get; set; }

}
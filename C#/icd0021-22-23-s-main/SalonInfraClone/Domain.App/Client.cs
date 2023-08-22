using System.Text.Json.Serialization;
using Domain.Base;

namespace Domain.App;

public class Client : DomainEntityId
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string PhoneNumber { get; set; } = default!;
    
    public ICollection<Record>? Records { get; set; }
    
    public ICollection<Invoice>? Invoices { get; set; }
}
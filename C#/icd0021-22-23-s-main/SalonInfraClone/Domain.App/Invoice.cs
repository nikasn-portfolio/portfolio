using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App;

public class Invoice : DomainEntityId
{
    public Guid? CompanyId { get; set; }
    
    public Company? Company { get; set; }
    
    public Guid InvoiceFooterId { get; set; }
    public InvoiceFooter? InvoiceFooter { get; set; }

    public Guid ClientId { get; set; } = default!;
    public Client? Client { get; set; }
    
    public Guid AppUserId { get; set; } = default!;
    public AppUser? AppUser {get;set;}

    public Guid RecordId { get; set; }
    public Record? Record { get; set; }
    
    public Guid PaymentMethodId { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }

    [MaxLength(200)] public string InvoiceNumber { get; set; } = Guid.NewGuid().ToString();
    public DateTime InvoiceDate { get; set; } = DateTime.Now;
    public DateTime? PaymentDate { get; set; }
    public bool IsCompany { get; set; } = false;
    [MaxLength(200)]
    public string? Comment { get; set; }
    
    
    public ICollection<InvoiceRow>? InvoiceRows { get; set; }
}
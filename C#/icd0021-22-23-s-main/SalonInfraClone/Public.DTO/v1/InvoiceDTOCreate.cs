using System.Collections;
using Domain.App;

namespace Public.DTO;

public class InvoiceDTOCreate
{
    public Guid? CompanyId { get; set; }
    public Guid InvoiceFooterId { get; set; }
    public Guid ClientId { get; set; } = default!;
    public Guid AppUserId { get; set; } = default!;
    public Guid RecordId { get; set; }
    public Guid PaymentMethodId { get; set; }
    public DateTimeOffset? PaymentDate { get; set; }
    public bool IsCompany { get; set; } = false;
    public string? Comment { get; set; }
    public ICollection<InvoiceRowDTO>? InvoiceRows { get; set; }
}
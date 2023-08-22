using Domain.Base;

namespace BLL.DTO;

public class InvoiceBLLDTO : DomainEntityId
{
    public DateTime InvoiceDate { get; set; } = default!;
    public DateTimeOffset? PaymentDate { get; set; } = default!;
    public PaymentMethodBLLDTO PaymentMethod { get; set; } = default!;
    public ClientBLLDTO Client { get; set; } = default!;
    public AppUserBLLDTO AppUser { get; set; } = default!;
    public RecordBLLDTO Record { get; set; } = default!;
    public ICollection<InvoiceRowBLLDTO>? InvoiceRows { get; set; }
    public InvoiceFooterBLLDTO InvoiceFooter { get; set; } = default!;
}
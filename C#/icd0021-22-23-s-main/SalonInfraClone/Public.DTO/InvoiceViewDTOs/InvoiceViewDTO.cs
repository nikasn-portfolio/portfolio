// ReSharper disable All

using Public.DTO.v1;

namespace Public.DTO.InvoiceViewDTOs;

public class InvoiceViewDTO
{
    public Guid Id { get; set; } = default!;
    public DateTimeOffset InvoiceDate { get; set; } = default!;
    public DateTimeOffset? PaymentDate { get; set; } = default!;
    public PaymentMethodDTO PaymentMethod { get; set; } = default!;
    public ClientDTO Client { get; set; } = default!;
    public AppUserDTO AppUser { get; set; } = default!;
    public RecordDTO Record { get; set; } = default!;
    public ICollection<InvoiceRowDTOonView>? InvoiceRows { get; set; }
    public InvoiceFooterDTOonView InvoiceFooter { get; set; } = default!;
}
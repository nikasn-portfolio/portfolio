using System.Collections;
using System.Collections.ObjectModel;
using Domain.App;
using Microsoft.VisualBasic;
using Public.DTO.v1;

namespace Public.DTO;

public class InvoicesDTOForListOnGet
{
    public Guid Id { get; set; } = default!;
    public DateTime InvoiceDate { get; set; } = default!;
    public DateTimeOffset? PaymentDate { get; set; } = default!;
    public PaymentMethodDTO PaymentMethod { get; set; } = default!;
    public ClientDTO Client { get; set; } = default!;
    public AppUserDTO AppUser { get; set; } = default!;
    public RecordDTO Record { get; set; } = default!;
    public Collection<string>? ServiceNames { get; set; }



}
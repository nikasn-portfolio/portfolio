using Domain.Base;

namespace BLL.DTO;

public class InvoiceFooterBLLDTO : DomainEntityId
{
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string Address { get; set; } = default!;
    public string? Iban { get; set; }
    public string CompanyName { get; set; } = default!;
}
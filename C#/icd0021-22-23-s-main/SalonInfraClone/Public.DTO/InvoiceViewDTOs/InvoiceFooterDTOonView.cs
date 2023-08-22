namespace Public.DTO.InvoiceViewDTOs;

public class InvoiceFooterDTOonView
{
    public Guid Id { get; set; } = default!;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string Address { get; set; } = default!;
    public string? Iban { get; set; } = default!;
    public string CompanyName { get; set; } = default!;
}
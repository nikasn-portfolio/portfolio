using Domain.Base;

namespace BLL.DTO;

public class CompanyBLLDTO : DomainEntityId
{
    public string CompanyName { get; set; } = default!;
    public string? IdentificationCode { get; set; }
    public string? Address { get; set; }
    public string? VatNumber { get; set; }
}
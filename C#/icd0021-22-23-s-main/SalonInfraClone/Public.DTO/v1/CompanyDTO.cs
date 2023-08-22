namespace Public.DTO;

public class CompanyDTO
{
    public Guid? Id { get; set; }
    public string CompanyName { get; set; } = default!;
    public string? IdentificationCode { get; set; }
    public string? Address { get; set; }
    public string? VatNumber { get; set; }
}
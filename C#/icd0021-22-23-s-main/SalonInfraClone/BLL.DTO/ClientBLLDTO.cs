using Domain.Base;

namespace BLL.DTO;

public class ClientBLLDTO : DomainEntityId
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string PhoneNumber { get; set; } = default!;
}
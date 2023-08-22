using Domain.Base;

namespace BLL.DTO;

public class AppUserBLLDTO : DomainEntityId
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string FullName { get; set; } = default!;
}
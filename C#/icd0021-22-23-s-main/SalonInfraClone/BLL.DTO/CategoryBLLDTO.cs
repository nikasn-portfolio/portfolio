using Domain.Base;

namespace BLL.DTO;

public class CategoryBLLDTO : DomainEntityId
{
    public string CategoryName { get; set; } = default!;
    public string CategoryImageUrl { get; set; } = default!;
    public ICollection<ServiceBLLDTO>? Services { get; set; }
}
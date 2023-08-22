using System.Collections;
using Domain.App;

namespace Public.DTO;

public class CategoryDTO
{
    public Guid Id { get; set; } = default!;
    public string CategoryName { get; set; } = default!;
    public string CategoryImageUrl { get; set; } = default!;
    public ICollection<ServiceDTO>? Services { get; set; }
}
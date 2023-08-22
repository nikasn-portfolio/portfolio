using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App;

public class Category : DomainEntityId
{
    [MaxLength(200)]
    public string CategoryName { get; set; } = default!;
    public string? CategoryImageUrl { get; set; }
    public ICollection<Service>? Services { get; set; }
}
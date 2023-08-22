using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App;

public class Language : DomainEntityId
{
    [MaxLength(200)]
    public string LanguageName { get; set; } = default!;
    public string? LanImageUrl { get; set; }
    
    public ICollection<Person>? Persons { get; set; }
}
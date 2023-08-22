using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App;

public class Person : DomainEntityId
{

    public Guid? LanguageId { get; set; }
    public Language? Language { get; set; }

    public Guid AppUserId { get; set; } = default!;
    public AppUser? AppUser {get;set;}

    [MaxLength(200)]
    public string PersonName { get; set; } = default!;
    [MaxLength(200)]
    public string PersonSurname { get; set; } = default!;
    [MaxLength(100)]
    public string? PersonPhoneNumber { get; set; }

    [DataType(DataType.Date)]
    [Column(TypeName = "Date")]
    public DateTime? PersonBirthDate { get; set; }


}
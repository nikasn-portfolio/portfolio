using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.Contracts.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.App.Identity;

public class AppUser : IdentityUser<Guid>, IDomainEntityId
{
    [MaxLength(128)]
    public string FirstName { get; set; } = default!;

    [MaxLength(128)]
    public string LastName { get; set; } = default!;
    
    public Person? Person { get; set; }
   
    public ICollection<Record>? Records { get; set; }
    
    public ICollection<Invoice>? Invoices { get; set; }
    public ICollection<AppRefreshToken>? AppRefreshTokens { get; set; }
}
namespace Domain.Base;

using System.ComponentModel.DataAnnotations;

public class BaseRefreshToken : BaseRefreshToken<Guid>
{
}

public class BaseRefreshToken<TKey> : DomainEntityId<TKey> 
    where TKey : struct, IEquatable<TKey>
{
    [MaxLength(64)]
    public string RefreshToken { get; set; } = Guid.NewGuid().ToString();
    public DateTime ExpirationDt { get; set; } = DateTime.UtcNow.AddDays(7);

    [MaxLength(64)]
    public string? PreviousRefreshToken { get; set; } 
    // UTC
    public DateTime? PreviousExpirationDt { get; set; } = DateTime.UtcNow.AddDays(7);

}
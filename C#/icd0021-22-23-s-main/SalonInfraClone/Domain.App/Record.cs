using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App;

public class Record : DomainEntityId
{

    public Guid AppUserId { get; set; } = default!;
    public AppUser? AppUser { get; set; }

    public Guid? ClientId { get; set; }
    public Client? Client { get; set; }
    
    
    [MaxLength(100)]
    public string Title { get; set; } = default!;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsHidden { get; set; } = false;
    [MaxLength(200)]
    public string? Comment { get; set; }

    //public ICollection<UserRecord>? UserRecords { get; set; }
    public Invoice? Invoice { get; set; }
    
    public ICollection<RecordService>? RecordServices { get; set; }
}
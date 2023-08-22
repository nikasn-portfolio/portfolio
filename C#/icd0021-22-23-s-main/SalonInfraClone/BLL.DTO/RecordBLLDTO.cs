using Domain.App;
using Domain.App.Identity;
using Domain.Base;

namespace BLL.DTO;

public class RecordBLLDTO : DomainEntityId
{
    public string? ServiceId { get; set; }
    public string Title { get; set; } = default!;
    public DateTimeOffset StartTime { get; set; } = default!;
    public DateTimeOffset EndTime { get; set; } = default!;

    public string Comment { get; set; } = default!;
    public string IsHidden { get; set; } = default!;
    public string AppUserId { get; set; } = default!;
    public AppUser? AppUser { get; set; }

    public ICollection<RecordServiceBLLDTO>? RecordServices { get; set; }

    public string ClientId { get; set; } = default!;
    public Client? Client { get; set; }
}
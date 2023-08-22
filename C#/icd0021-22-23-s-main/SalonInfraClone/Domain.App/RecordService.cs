using System.Text.Json.Serialization;
using Domain.Base;

namespace Domain.App;

public class RecordService : DomainEntityId
{
    public Guid RecordId { get; set; } = default!;
    public Record? Record { get; set; }

    public Guid ServiceId { get; set; } = default!;
    public Service? Service { get; set; }
}
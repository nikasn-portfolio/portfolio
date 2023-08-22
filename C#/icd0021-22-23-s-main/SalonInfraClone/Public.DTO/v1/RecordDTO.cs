using System.Collections;
using Domain.App;

namespace Public.DTO.v1;

public class RecordDTO
{
    public string? Id { get; set; }
    
    public string? ServiceId { get; set; }
    public string Title { get; set; } = default!;
    public DateTimeOffset StartTime { get; set; } = default!;
    public DateTimeOffset EndTime { get; set; } = default!;

    public string Comment { get; set; } = default!;
    public string IsHidden { get; set; } = default!;
    public string AppUserId { get; set; } = default!;

    public ICollection<RecordServiceDTO>? RecordServices { get; set; }

    public string ClientId { get; set; } = default!;
}
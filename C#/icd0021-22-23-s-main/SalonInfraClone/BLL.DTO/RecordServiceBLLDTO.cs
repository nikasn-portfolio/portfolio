using Domain.Base;

namespace BLL.DTO;

public class RecordServiceBLLDTO : DomainEntityId
{
    public Guid RecordId { get; set; } = default!;
    public RecordBLLDTO? Record { get; set; }

    public Guid ServiceId { get; set; } = default!;
    public ServiceBLLDTO? Service { get; set; }

}
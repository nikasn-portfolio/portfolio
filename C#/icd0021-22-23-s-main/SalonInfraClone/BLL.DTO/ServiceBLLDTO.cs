using Domain.Base;

namespace BLL.DTO;

public class ServiceBLLDTO : DomainEntityId
{
    
        public Guid CategoryId { get; set; } = default!;
        public string ServiceName { get; set; } = default!;
        public TimeSpan? ServiceDuration { get; set; }
        public double ServicePrice { get; set; } = default!;
    
}
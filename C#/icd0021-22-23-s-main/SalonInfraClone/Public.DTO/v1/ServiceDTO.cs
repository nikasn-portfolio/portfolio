namespace Public.DTO;

public class ServiceDTO
{
    public Guid Id { get; set; } = default!;
    public Guid CategoryId { get; set; } = default!;
    public string ServiceName { get; set; } = default!;
    public TimeSpan? ServiceDuration { get; set; }
    public double ServicePrice { get; set; } = default!;
}
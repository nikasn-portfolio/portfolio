namespace Public.DTO;

public class PaymentMethodDTO
{
    public Guid? Id { get; set; }
    public string PaymentMethodName { get; set; } = default!;
    public string? MethodImageUrl { get; set; }
}
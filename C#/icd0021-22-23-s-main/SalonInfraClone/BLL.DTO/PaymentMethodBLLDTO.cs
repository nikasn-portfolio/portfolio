using Domain.Base;

namespace BLL.DTO;

public class PaymentMethodBLLDTO : DomainEntityId
{
    public string PaymentMethodName { get; set; } = default!;
    public string? MethodImageUrl { get; set; }
}
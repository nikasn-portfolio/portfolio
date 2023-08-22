using Domain.Base;

namespace BLL.DTO;

public class InvoiceRowBLLDTO : DomainEntityId
{
    
    public int Quantity { get; set; } = 1;
    
    public Guid? ServiceId { get; set; }


    public ServiceBLLDTO? Service { get; set; }
    
    public decimal? PriceOverride { get; set; } = default!;
    
    public decimal Tax { get; set; } = default!;
    
    public decimal Total { get; set; } = default!;
}
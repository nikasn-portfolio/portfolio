namespace Public.DTO;

public class InvoiceRowDTO
{
    public Guid ServiceId { get; set; } = default!;

    public int Quantity { get; set; } = 1;
    
    public decimal? PriceOverride { get; set; } = default!;
    
    public decimal Tax { get; set; } = default!;
    
    public decimal Total { get; set; } = default!;
}
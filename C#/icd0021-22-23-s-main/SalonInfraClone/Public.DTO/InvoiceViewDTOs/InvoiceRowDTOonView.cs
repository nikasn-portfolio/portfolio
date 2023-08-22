using System.Collections.ObjectModel;
using Microsoft.VisualBasic;

namespace Public.DTO.InvoiceViewDTOs;

public class InvoiceRowDTOonView
{
    public Guid Id { get; set; } = default!;
    
    public int Quantity { get; set; } = 1;

    public ServiceDTO? Service { get; set; }
    
    public decimal? PriceOverride { get; set; } = default!;
    
    public decimal Tax { get; set; } = default!;
    
    public decimal Total { get; set; } = default!;
}
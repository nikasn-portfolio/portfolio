using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.Base;

namespace Domain.App;

public class Service : DomainEntityId
{

    public Guid CategoryId { get; set; }
    // JsonIgnore Needs for retrieve Services and Category they belongs via api
    public Category? Category { get; set; }

    [MaxLength(200)]
    public string ServiceName { get; set; } = default!;
    
    // avarage time property in time format
    
    [DataType(DataType.Time)]
    public TimeSpan? ServiceDuration { get; set; }
    
    public double ServicePrice { get; set; }

    public ICollection<InvoiceRow>? InvoiceRows { get; set; }
    public ICollection<RecordService>? RecordServices { get; set; }
}
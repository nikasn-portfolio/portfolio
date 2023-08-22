using AutoMapper;
using BLL.DTO;
using DAL.Base;
using Domain.App;

namespace Public.DTO.Mappers;

public class InvoiceMapper : BaseMapper<InvoiceDTOCreate, Invoice>
{
    public InvoiceMapper(IMapper mapper) : base(mapper)
    {
    }
}
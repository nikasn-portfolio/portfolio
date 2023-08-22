using AutoMapper;
using BLL.DTO;
using DAL.Base;
using Domain.App;

namespace Public.DTO.Mappers;

public class InvoiceOnGetMapper : BaseMapper<InvoicesDTOForListOnGet, InvoiceBLLDTO>
{
    public InvoiceOnGetMapper(IMapper mapper) : base(mapper)
    {
    }
}
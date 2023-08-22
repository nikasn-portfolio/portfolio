using AutoMapper;
using BLL.DTO;
using DAL.Base;
using Domain.App;

namespace Public.DTO.Mappers;

public class InvoiceFooterCreateMapper : BaseMapper<InvoiceFooterDTOCreate, InvoiceFooterBLLDTO>
{
    public InvoiceFooterCreateMapper(IMapper mapper) : base(mapper)
    {
    }
}
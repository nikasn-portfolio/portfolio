using AutoMapper;
using BLL.DTO;
using DAL.Base;
using Domain.App;
using Public.DTO.InvoiceViewDTOs;

namespace Public.DTO.Mappers;

public class InvoiceOnViewMapper : BaseMapper<InvoiceViewDTO, InvoiceBLLDTO>
{
    public InvoiceOnViewMapper(IMapper mapper) : base(mapper)
    {
    }
}
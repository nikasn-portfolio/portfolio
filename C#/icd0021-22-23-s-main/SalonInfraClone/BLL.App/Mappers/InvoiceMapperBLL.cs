using AutoMapper;
using BLL.DTO;
using DAL.Base;
using Domain.App;

namespace BLL.App.Mappers;

public class InvoiceMapperBLL : BaseMapper<InvoiceBLLDTO, Invoice>
{
    public InvoiceMapperBLL(IMapper mapper) : base(mapper)
    {
    }
}
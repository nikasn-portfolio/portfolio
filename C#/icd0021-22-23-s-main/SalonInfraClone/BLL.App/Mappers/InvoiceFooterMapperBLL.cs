using AutoMapper;
using BLL.DTO;
using DAL.Base;
using Domain.App;

namespace BLL.App.Mappers;

public class InvoiceFooterMapperBLL : BaseMapper<InvoiceFooterBLLDTO, InvoiceFooter>
{
    public InvoiceFooterMapperBLL(IMapper mapper) : base(mapper)
    {
    }
}
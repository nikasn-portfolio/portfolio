using AutoMapper;
using BLL.DTO;
using Domain.App;
using Domain.App.Identity;
using Public.DTO.InvoiceViewDTOs;
using Public.DTO.v1;

namespace Public.DTO;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<RecordDTO, RecordBLLDTO>().ReverseMap();
        CreateMap<RecordServiceDTO, RecordServiceBLLDTO>().ReverseMap();
        CreateMap<RecordService, RecordServiceBLLDTO>().ReverseMap();
        CreateMap<CategoryDTO, CategoryBLLDTO>().ReverseMap();
        CreateMap<ServiceDTO, ServiceBLLDTO>().ReverseMap();
        CreateMap<Invoice, InvoiceDTOCreate>().ReverseMap().ForMember(dest => dest.PaymentDate,
            options => options.MapFrom(src => src.PaymentDate!.Value.DateTime));
        CreateMap<InvoiceRowDTO, InvoiceRowBLLDTO>().ReverseMap();
        CreateMap<InvoiceRowDTO, InvoiceRow>().ReverseMap();
        CreateMap<InvoiceRowDTOonView, InvoiceRowBLLDTO>().ReverseMap();
        CreateMap<PaymentMethodDTO, PaymentMethodBLLDTO>().ReverseMap();
        CreateMap<CompanyDTO, CompanyBLLDTO>().ReverseMap();
        CreateMap<InvoiceFooterDTOCreate, InvoiceFooterBLLDTO>().ReverseMap();
        CreateMap<InvoiceFooterDTOonView, InvoiceFooterBLLDTO>().ReverseMap();
        CreateMap<ClientDTO, ClientBLLDTO>().ReverseMap().ForMember(
            dest => dest.FullName,
            options => options.MapFrom(src => src.FirstName + " " + src.LastName)
            );
        CreateMap<AppUserDTO, AppUserBLLDTO>().ReverseMap().ForMember(
            dest => dest.FullName,
            options => options.MapFrom(src => src.FirstName + " " + src.LastName)
        );
        CreateMap<InvoicesDTOForListOnGet, InvoiceBLLDTO>().ReverseMap().ForMember(
            dest => dest.ServiceNames,
            options => options.MapFrom(src =>src.Record!.RecordServices!.Select(e => e.Service!.ServiceName))
            );
        CreateMap<InvoiceViewDTO, InvoiceBLLDTO>().ReverseMap();
    }
}
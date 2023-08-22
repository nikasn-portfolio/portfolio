using AutoMapper;
using BLL.DTO;
using Domain.App;
using Domain.App.Identity;

namespace BLL.App;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<CategoryBLLDTO, Category>().ReverseMap();
        CreateMap<ServiceBLLDTO, Service>().ReverseMap();
        CreateMap<ClientBLLDTO, Client>().ReverseMap();
        CreateMap<CompanyBLLDTO, Company>().ReverseMap();
        CreateMap<InvoiceFooterBLLDTO, InvoiceFooter>().ReverseMap();
        CreateMap<AppUserBLLDTO, AppUser>().ReverseMap();
        CreateMap<InvoiceBLLDTO, Invoice>().ReverseMap();
        CreateMap<InvoiceRowBLLDTO, InvoiceRow>().ReverseMap();
        CreateMap<PaymentMethodBLLDTO, PaymentMethod>().ReverseMap();
        CreateMap<RecordBLLDTO, Record>().ReverseMap();
        CreateMap<RecordServiceBLLDTO, RecordService>().ReverseMap();
    }
}
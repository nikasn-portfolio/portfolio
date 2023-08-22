using AutoMapper;
using BLL.App.Mappers;
using BLL.App.Services;
using BLL.Base;
using BLL.Contracts.App;
using DAL.Contracts.App;

namespace BLL.App;

public class AppBLL : BaseBLL<IAppUOW>, IAppBLL
{
    protected IAppUOW Uow;
    private readonly AutoMapper.IMapper _mapper;
    
    public AppBLL(IAppUOW uow, IMapper mapper) : base(uow)
    {
        _mapper = mapper;
        Uow = uow;
    }

    private ICategoryService? _categoryService;

    public ICategoryService CategoryService =>
        _categoryService ??= new CategoryService(Uow, new CategoryMapperBLL(_mapper));
    
    private IClientService? _clientService;

    public IClientService ClientService =>
        _clientService ??= new ClientService(Uow, new ClientMapperBLL(_mapper));
    
    private ICompanyService? _companyService;

    public ICompanyService CompanyService =>
        _companyService ??= new CompanyService(Uow, new CompanyMapperBLL(_mapper));
    
    private IInvoiceFooterService? _invoiceFooterService;

    public IInvoiceFooterService InvoiceFooterService =>
        _invoiceFooterService ??= new InvoiceFooterService(Uow, new InvoiceFooterMapperBLL(_mapper));
    
    private IInvoiceService? _invoiceService;

    public IInvoiceService InvoiceService =>
        _invoiceService ??= new InvoiceService(Uow, new InvoiceMapperBLL(_mapper));
    
    private IPaymentMethodService? _paymentMethod;

    public IPaymentMethodService PaymentMethodService =>
        _paymentMethod??= new PaymentMethodService(Uow, new PaymentMethodMapperBLL(_mapper));
    
    private IRecordService? _recordService;

    public IRecordService RecordService =>
        _recordService??= new RecordService(Uow, new RecordMapperBLL(_mapper));
    
    private IRecordServiceService? _recordServiceService;

    public IRecordServiceService RecordServiceService =>
        _recordServiceService??= new RecordServiceService(Uow, new RecordServiceServiceMapperBLL(_mapper));

    private IServiceService? _service;

    public IServiceService Service =>
        _service??= new ServiceService(Uow, new ServiceMapperBLL(_mapper));
}
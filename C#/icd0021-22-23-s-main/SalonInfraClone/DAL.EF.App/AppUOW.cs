using DAL.Contracts.App;
using DAL.Contracts.Base;
using DAL.EF.Base;
using DAL.Repository;

namespace DAL;

public class AppUOW : EFBaseUOW<ApplicationDbContext>, IAppUOW
{
    public AppUOW(ApplicationDbContext dataContext) : base(dataContext)
    {
        
    }

    private ICategoryRepository? _categoryRepository;

    public ICategoryRepository CategoryRepository =>
        _categoryRepository ??= new CategoryRepository(_uowDbContext);

    private ICompanyRepository? _companyRepository;
    
    public ICompanyRepository CompanyRepository =>
        _companyRepository ??= new CompanyRepository(_uowDbContext);

    

    private IInvoiceFooterRepository? _invoiceFooterRepository;
    
    public IInvoiceFooterRepository InvoiceFooterRepository =>
        _invoiceFooterRepository ??= new InvoiceFooterRepository(_uowDbContext);
    
    private IInvoiceRepository? _invoiceRepository;

    public IInvoiceRepository InvoiceRepository =>
        _invoiceRepository ??= new InvoiceRepository(_uowDbContext);

    private IInvoiceRowRepository? _invoiceRowRepository;

    public IInvoiceRowRepository InvoiceRowRepository =>
        _invoiceRowRepository ??= new InvoiceRowRepository(_uowDbContext);

    private ILanguageRepository? _languageRepository;

    public ILanguageRepository LanguageRepository =>
        _languageRepository ??= new LanguageRepository(_uowDbContext);

    private IPaymentMethodRepository? _paymentMethodRepository;

    public IPaymentMethodRepository PaymentMethodRepository =>
        _paymentMethodRepository ??= new PaymentMethodRepository(_uowDbContext);

    private IPersonRepository? _personRepository;

    public IPersonRepository PersonRepository =>
        _personRepository ??= new PersonRepository(_uowDbContext);

    private IRecordRepository? _recordRepository;

    public IRecordRepository RecordRepository =>
        _recordRepository ??= new RecordRepository(_uowDbContext);

    private IServiceRepository? _serviceRepository;

    public IServiceRepository ServiceRepository =>
        _serviceRepository ??= new ServiceRepository(_uowDbContext);
    
    private IRecordServiceRepository? _recordServiceRepository;

    public IRecordServiceRepository RecordServiceRepository =>
        _recordServiceRepository ??= new RecordServiceRepository(_uowDbContext);

    private IClientRepository? _clientRepository;

    public IClientRepository ClientRepository => _clientRepository ??= new ClientRepository(_uowDbContext);

    /*private IUserRecordRepository? _userRecordRepository;
    
    public IUserRecordRepository UserRecordRepository =>
        _userRecordRepository ??= new UserRecordRepository(_uowDbContext);*/
}
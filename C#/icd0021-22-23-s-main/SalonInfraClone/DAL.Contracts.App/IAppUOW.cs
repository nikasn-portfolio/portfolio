using DAL.Contracts.Base;

namespace DAL.Contracts.App;

public interface IAppUOW : IBaseUOW
{
    ICategoryRepository CategoryRepository { get; }
    ICompanyRepository CompanyRepository { get; }
    IInvoiceFooterRepository InvoiceFooterRepository { get; }
    IInvoiceRepository InvoiceRepository { get; }
    IInvoiceRowRepository InvoiceRowRepository { get; }
    ILanguageRepository LanguageRepository { get; }
    IPaymentMethodRepository PaymentMethodRepository { get; }
    IPersonRepository PersonRepository { get; }
    IRecordRepository RecordRepository { get; }
    IServiceRepository ServiceRepository { get; }
    IClientRepository ClientRepository { get; }
    IRecordServiceRepository RecordServiceRepository { get; }
    //IUserRecordRepository UserRecordRepository { get; }
}
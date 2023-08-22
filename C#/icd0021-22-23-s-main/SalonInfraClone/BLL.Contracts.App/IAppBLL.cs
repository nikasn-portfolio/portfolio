using BLL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IAppBLL : IBaseBLL
{
    ICategoryService CategoryService { get; }
    IClientService ClientService { get; }
    ICompanyService CompanyService { get; }
    IInvoiceFooterService InvoiceFooterService { get; }
    IInvoiceService InvoiceService { get; }
    IPaymentMethodService PaymentMethodService { get; }
    IRecordService RecordService { get; }
    IRecordServiceService RecordServiceService { get; }
    
    IServiceService Service { get; }

}
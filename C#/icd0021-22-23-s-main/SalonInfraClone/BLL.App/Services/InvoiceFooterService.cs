using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO;
using Contracts.Base;
using DAL.Contracts.App;
using Domain.App;

namespace BLL.App.Services;

public class InvoiceFooterService : BaseEntityService<InvoiceFooterBLLDTO,InvoiceFooter, IInvoiceFooterRepository>, IInvoiceFooterService
{
    protected IAppUOW Uow;
    public InvoiceFooterService(IAppUOW uow, IMapper<InvoiceFooterBLLDTO, InvoiceFooter> mapper) : base(uow.InvoiceFooterRepository, mapper)
    {
        Uow = uow;
    }
}
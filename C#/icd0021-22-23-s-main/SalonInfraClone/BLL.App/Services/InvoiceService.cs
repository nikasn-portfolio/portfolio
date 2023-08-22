using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO;
using Contracts.Base;
using DAL.Contracts.App;
using Domain.App;

namespace BLL.App.Services;

public class InvoiceService : BaseEntityService<InvoiceBLLDTO,Invoice, IInvoiceRepository>, IInvoiceService
{
    protected IAppUOW Uow;
    public InvoiceService(IAppUOW uow, IMapper<InvoiceBLLDTO, Invoice> mapper) : base(uow.InvoiceRepository, mapper)
    {
        Uow = uow;
    }

    public async Task<IEnumerable<InvoiceBLLDTO>> AllInvoicesWithIncludes()
    {
        var x = await Uow.InvoiceRepository.AllInvoicesWithIncludes();
        var y = x!.Select(e => Mapper.Map(e));
        
        return y!;
    }

    public Invoice Add(Invoice invoice)
    {
       return Uow.InvoiceRepository.Add(invoice);
    }

    public async Task<Invoice?> GetInvoiceById(Guid id)
    {
        return await Uow.InvoiceRepository.FindAsync(id);
    }

    public Invoice Remove(Invoice invoice)
    {
        return Uow.InvoiceRepository.Remove(invoice);
    }
}
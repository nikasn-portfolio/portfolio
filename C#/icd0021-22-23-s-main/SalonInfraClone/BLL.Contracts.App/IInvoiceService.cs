using BLL.DTO;
using DAL.Contracts.App;
using DAL.Contracts.Base;
using Domain.App;

namespace BLL.Contracts.App;

public interface IInvoiceService : IBaseRepository<InvoiceBLLDTO>,IInvoiceRepositoryCustom<InvoiceBLLDTO>
{
    Invoice Add(Invoice invoice);

    Task<Invoice?> GetInvoiceById(Guid id);

    Invoice Remove(Invoice invoice);
}
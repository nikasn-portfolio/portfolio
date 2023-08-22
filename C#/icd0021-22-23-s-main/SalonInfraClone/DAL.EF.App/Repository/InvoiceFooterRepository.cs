using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;

namespace DAL.Repository;

public class InvoiceFooterRepository : EFBaseRepository<InvoiceFooter,ApplicationDbContext>, IInvoiceFooterRepository
{
    public InvoiceFooterRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
}
using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;

namespace DAL.Repository;

public class InvoiceRowRepository : EFBaseRepository<InvoiceRow,ApplicationDbContext>, IInvoiceRowRepository
{
    public InvoiceRowRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
}
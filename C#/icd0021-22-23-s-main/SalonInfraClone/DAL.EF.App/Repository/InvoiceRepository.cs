using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository;

public class InvoiceRepository : EFBaseRepository<Invoice,ApplicationDbContext>, IInvoiceRepository
{
    public InvoiceRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public async Task<IEnumerable<Invoice>> AllInvoicesWithIncludes()
    {
        return await RepositoryDbSet.Include(i => i.Client)
            .Include(i => i.Company)
            .Include(i => i.Record).ThenInclude(r => r!.RecordServices)!.ThenInclude(s => s.Service)
            .Include(i => i.AppUser)
            .Include(i => i.InvoiceFooter)
            .Include(i => i.InvoiceRows)!.ThenInclude(e => e.Service)
            .Include(i => i.PaymentMethod)
            .ToListAsync();
    }

    public override async Task<Invoice?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(s => s.InvoiceFooter)
            .Include(s => s.Record)
            .Include(s => s.InvoiceRows)!
            .ThenInclude(s => s.Service)
            .FirstOrDefaultAsync(s => s.Id == id);
    }
}
using DAL.Contracts.Base;
using Domain.App;

namespace DAL.Contracts.App;

public interface IInvoiceRepository : IBaseRepository<Invoice>, IInvoiceRepositoryCustom<Invoice>
{
    
}

public interface IInvoiceRepositoryCustom<TEntity>
{
    // add here shared methods between repo and service
    Task<IEnumerable<TEntity>> AllInvoicesWithIncludes();
    
}
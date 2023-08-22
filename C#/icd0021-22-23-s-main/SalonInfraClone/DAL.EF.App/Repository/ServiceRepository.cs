using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;

namespace DAL.Repository;

public class ServiceRepository : EFBaseRepository<Service,ApplicationDbContext>, IServiceRepository
{
    public ServiceRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
}
using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;

namespace DAL.Repository;

public class ClientRepository : EFBaseRepository<Client, ApplicationDbContext>, IClientRepository
{
    public ClientRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
}
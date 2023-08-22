using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;

namespace DAL.Repository;

public class PersonRepository : EFBaseRepository<Person,ApplicationDbContext>, IPersonRepository
{
    public PersonRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
}
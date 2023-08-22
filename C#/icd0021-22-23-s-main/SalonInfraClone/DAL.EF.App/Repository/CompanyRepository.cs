using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;

namespace DAL.Repository;

public class CompanyRepository : EFBaseRepository<Company,ApplicationDbContext>, ICompanyRepository
{
    public CompanyRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
}
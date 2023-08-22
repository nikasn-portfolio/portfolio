using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;

namespace DAL.Repository;

public class LanguageRepository : EFBaseRepository<Language,ApplicationDbContext>, ILanguageRepository
{
    public LanguageRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
}
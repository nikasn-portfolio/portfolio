using DAL.Contracts.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Base;

public class EFBaseUOW<TDbContext> : IBaseUOW
    where TDbContext : DbContext
{
    protected readonly TDbContext _uowDbContext;

    public EFBaseUOW(TDbContext dataContext)
    {
        _uowDbContext = dataContext;
    }

    public virtual async Task<int> SaveChangesAsync()
    {
        return await _uowDbContext.SaveChangesAsync();
    }
}
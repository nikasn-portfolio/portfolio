using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;
using Public.DTO.v1;

namespace DAL.Repository;

public class RecordRepository : EFBaseRepository<Record,ApplicationDbContext>, IRecordRepository
{
    public RecordRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public async Task<IEnumerable<Record>> AllAsyncWithServices()
    {
        return await RepositoryDbSet.Include(s => s.RecordServices).ToListAsync();
    }
}
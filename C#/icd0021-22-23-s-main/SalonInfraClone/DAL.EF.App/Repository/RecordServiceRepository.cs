using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository;

public class RecordServiceRepository : EFBaseRepository<RecordService,ApplicationDbContext>, IRecordServiceRepository
{
    public RecordServiceRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public async Task<List<RecordService>> AllAsyncWithServices()
    {
        return await RepositoryDbSet.Include(s => s.Record).Include(s => s.Service).ToListAsync();
    }

    public void RemoveListOfRecordServicesAsync(List<RecordService> list)
    {
        foreach (var recordService in list)
        {
            RepositoryDbSet.Remove(recordService);
        }

        RepositoryDbContext.SaveChanges();
    }

    public RecordService FindRecordServiceByChild(Guid recordId, Guid serviceId)
    {
        return RepositoryDbSet.FirstOrDefault(r => r.ServiceId == serviceId && r.RecordId == recordId)!;
    }
}
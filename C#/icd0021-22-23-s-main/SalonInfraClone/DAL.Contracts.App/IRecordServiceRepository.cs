using DAL.Contracts.Base;
using Domain.App;

namespace DAL.Contracts.App;

public interface IRecordServiceRepository : IBaseRepository<RecordService>, IRecordServiceRepositoryCustom<RecordService>
{
    
}

public interface IRecordServiceRepositoryCustom<TEntity>
{
    Task<List<TEntity>> AllAsyncWithServices();
    void RemoveListOfRecordServicesAsync(List<RecordService> list);
    RecordService FindRecordServiceByChild(Guid recordId, Guid serviceId);
}
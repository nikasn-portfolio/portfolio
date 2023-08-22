using DAL.Contracts.Base;
using Domain.App;

namespace DAL.Contracts.App;

public interface IRecordRepository : IBaseRepository<Record>, IRecordRepositoryCustom<Record>
{
   
}

public interface IRecordRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> AllAsyncWithServices();
}
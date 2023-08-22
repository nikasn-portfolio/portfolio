using BLL.DTO;
using DAL.Contracts.App;
using DAL.Contracts.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BLL.Contracts.App;

public interface IRecordService : IBaseRepository<RecordBLLDTO>, IRecordRepositoryCustom<RecordBLLDTO>
{
    Task<IEnumerable<Record>> GetRecordsWithServices();
    Task<Record> GetRecordById(Guid id);
    EntityEntry<Record> SetEntry(Record record);

    Record Add(Record record);

    Record Remove(Record record);

    Record Update(Record record);
}
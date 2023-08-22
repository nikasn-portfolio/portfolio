using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO;
using Contracts.Base;
using DAL.Contracts.App;
using Domain.App;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BLL.App.Services;

public class RecordService : BaseEntityService<RecordBLLDTO, Record , IRecordRepository>, IRecordService
{
    protected IAppUOW Uow;
    public RecordService(IAppUOW uow, IMapper<RecordBLLDTO, Record> mapper) : base(uow.RecordRepository, mapper)
    {
        Uow = uow;
    }


    public async Task<IEnumerable<RecordBLLDTO>> AllAsyncWithServices()
    {
        return (await Uow.RecordRepository.AllAsyncWithServices()).Select(e => Mapper.Map(e))!;
    }

    public async Task<IEnumerable<Record>> GetRecordsWithServices()
    {
        return await Uow.RecordRepository.AllAsyncWithServices();
    }

    public async Task<Record> GetRecordById(Guid id)
    {
        return (await Uow.RecordRepository.FindAsync(id))!;
    }

    public EntityEntry<Record> SetEntry(Record record)
    {
        return Uow.RecordRepository.Entry(record);
    }

    public Record Add(Record record)
    {
        return Uow.RecordRepository.Add(record);
    }

    public Record Remove(Record record)
    {
        return Uow.RecordRepository.Remove(record);
    }

    public Record Update(Record record)
    {
        return Uow.RecordRepository.Update(record);
    }
}
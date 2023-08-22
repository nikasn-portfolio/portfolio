using BLL.DTO;
using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IRecordServiceService : IBaseRepository<RecordServiceBLLDTO>, IRecordServiceRepositoryCustom<RecordServiceBLLDTO>
{
    void Add(Domain.App.RecordService recordService);
}
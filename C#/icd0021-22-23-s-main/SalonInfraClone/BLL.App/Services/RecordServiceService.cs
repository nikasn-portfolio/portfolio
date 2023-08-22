using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO;
using Contracts.Base;
using DAL.Contracts.App;

namespace BLL.App.Services;

public class RecordServiceService : BaseEntityService<RecordServiceBLLDTO, Domain.App.RecordService, IRecordServiceRepository>, IRecordServiceService
{
    protected IAppUOW Uow;
    public RecordServiceService(IAppUOW uow, IMapper<RecordServiceBLLDTO, Domain.App.RecordService> mapper) : base(uow.RecordServiceRepository, mapper)
    {
        Uow = uow;
    }

    public async Task<List<RecordServiceBLLDTO>> AllAsyncWithServices()
    {
        var x = await Uow.RecordServiceRepository.AllAsyncWithServices();
        var y = x.Select(e => Mapper.Map(e)).ToList();
        return y!;
    }

    public void RemoveListOfRecordServicesAsync(List<Domain.App.RecordService> list)
    {
        Uow.RecordServiceRepository.RemoveListOfRecordServicesAsync(list);
    }

    Domain.App.RecordService IRecordServiceRepositoryCustom<RecordServiceBLLDTO>.FindRecordServiceByChild(Guid recordId, Guid serviceId)
    {
        return Uow.RecordServiceRepository.FindRecordServiceByChild(recordId, serviceId);
    }

    public void Add(Domain.App.RecordService recordService)
    {
        Uow.RecordServiceRepository.Add(recordService);
    }
}
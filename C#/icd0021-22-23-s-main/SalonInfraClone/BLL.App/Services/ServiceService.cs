using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO;
using Contracts.Base;
using DAL.Contracts.App;
using Domain.App;

namespace BLL.App.Services;

public class ServiceService : BaseEntityService<ServiceBLLDTO,Service, IServiceRepository>, IServiceService
{
    protected IAppUOW Uow;
    public ServiceService(IAppUOW uow, IMapper<ServiceBLLDTO, Service> mapper) : base(uow.ServiceRepository, mapper)
    {
        Uow = uow;
    }

    public async Task<Service?> GetServiceById(Guid id)
    {
        return await Uow.ServiceRepository.FindAsync(id);
    }
}
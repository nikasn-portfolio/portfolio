using BLL.DTO;
using DAL.Contracts.Base;
using Domain.App;

namespace BLL.Contracts.App;

public interface IServiceService : IBaseRepository<ServiceBLLDTO>
{
    Task<Service?> GetServiceById(Guid id);
}
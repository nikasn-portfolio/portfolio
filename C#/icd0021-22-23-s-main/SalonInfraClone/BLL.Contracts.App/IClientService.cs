using BLL.DTO;
using DAL.Contracts.Base;
using Domain.App;

namespace BLL.Contracts.App;

public interface IClientService : IBaseRepository<ClientBLLDTO>
{
    Task<Client> GetClientById(Guid id);
}
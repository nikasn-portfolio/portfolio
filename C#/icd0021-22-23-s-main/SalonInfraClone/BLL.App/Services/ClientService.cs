using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO;
using Contracts.Base;
using DAL.Contracts.App;
using Domain.App;

namespace BLL.App.Services;

public class ClientService : BaseEntityService<ClientBLLDTO,Client, IClientRepository>, IClientService
{
    protected IAppUOW Uow;
    public ClientService(IAppUOW uow, IMapper<ClientBLLDTO, Client> mapper) : base(uow.ClientRepository, mapper)
    {
        Uow = uow;
    }

    public Task<Client> GetClientById(Guid id)
    {
        var x = Uow.ClientRepository.FindAsync(id);
        return x!;
    }
}
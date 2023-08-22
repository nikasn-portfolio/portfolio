using AutoMapper;
using BLL.DTO;
using DAL.Base;
using Domain.App;

namespace BLL.App.Mappers;

public class ClientMapperBLL : BaseMapper<ClientBLLDTO, Client>
{
    public ClientMapperBLL(IMapper mapper) : base(mapper)
    {
    }
}
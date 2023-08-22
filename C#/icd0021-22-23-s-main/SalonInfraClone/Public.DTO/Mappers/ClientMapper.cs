using AutoMapper;
using BLL.DTO;
using DAL.Base;
using Domain.App;

namespace Public.DTO.Mappers;

public class ClientMapper : BaseMapper<ClientDTO, ClientBLLDTO>
{
    public ClientMapper(IMapper mapper) : base(mapper)
    {
    }
    
}
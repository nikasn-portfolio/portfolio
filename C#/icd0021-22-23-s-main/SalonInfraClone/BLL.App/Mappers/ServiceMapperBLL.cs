using AutoMapper;
using BLL.DTO;
using DAL.Base;
using Domain.App;

namespace BLL.App.Mappers;

public class ServiceMapperBLL : BaseMapper<ServiceBLLDTO, Service>
{
    public ServiceMapperBLL(IMapper mapper) : base(mapper)
    {
    }
}
using AutoMapper;
using BLL.DTO;
using DAL.Base;
using Domain.App.Identity;

namespace Public.DTO.Mappers;

public class WorkerMapper : BaseMapper<AppUserDTO, AppUserBLLDTO>
{
    public WorkerMapper(IMapper mapper) : base(mapper)
    {
    }
}
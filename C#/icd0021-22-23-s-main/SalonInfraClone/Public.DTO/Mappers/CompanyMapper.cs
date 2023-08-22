using AutoMapper;
using BLL.DTO;
using DAL.Base;
using Domain.App;

namespace Public.DTO.Mappers;

public class CompanyMapper : BaseMapper<CompanyDTO, CompanyBLLDTO>
{
    public CompanyMapper(IMapper mapper) : base(mapper)
    {
    }
}
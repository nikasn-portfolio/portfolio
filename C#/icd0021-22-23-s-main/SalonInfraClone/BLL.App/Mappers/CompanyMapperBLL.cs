using AutoMapper;
using BLL.DTO;
using DAL.Base;
using Domain.App;

namespace BLL.App.Mappers;

public class CompanyMapperBLL : BaseMapper<CompanyBLLDTO, Company>
{
    public CompanyMapperBLL(IMapper mapper) : base(mapper)
    {
    }
}
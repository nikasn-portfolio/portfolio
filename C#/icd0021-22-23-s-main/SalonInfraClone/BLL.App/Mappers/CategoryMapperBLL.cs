using AutoMapper;
using BLL.DTO;
using DAL.Base;
using Domain.App;

namespace BLL.App.Mappers;

public class CategoryMapperBLL : BaseMapper<CategoryBLLDTO, Category>
{
    public CategoryMapperBLL(IMapper mapper) : base(mapper)
    {
    }
}
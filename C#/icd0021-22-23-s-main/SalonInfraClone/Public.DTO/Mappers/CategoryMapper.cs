using AutoMapper;
using BLL.DTO;
using DAL.Base;
using Domain.App;

namespace Public.DTO.Mappers;

public class CategoryMapper : BaseMapper<CategoryDTO, CategoryBLLDTO>
{
    public CategoryMapper(IMapper mapper) : base(mapper)
    {
    }
}
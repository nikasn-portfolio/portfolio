using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO;
using Contracts.Base;
using DAL.Contracts.App;
using Domain.App;

namespace BLL.App.Services;

public class CategoryService : BaseEntityService<CategoryBLLDTO, Category, ICategoryRepository>, ICategoryService
{
    protected IAppUOW Uow;
    public CategoryService(IAppUOW uow, IMapper<CategoryBLLDTO, Category> mapper) : base(uow.CategoryRepository, mapper)
    {
        Uow = uow;
    }

    public async Task<IEnumerable<CategoryBLLDTO>> AllAsync()
    {
        var data = (await Uow.CategoryRepository.AllAsync()).Select(e => Mapper.Map(e));
        return data!;
    }
}
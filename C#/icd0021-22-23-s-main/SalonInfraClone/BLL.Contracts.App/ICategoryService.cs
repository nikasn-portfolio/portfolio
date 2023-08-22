using BLL.DTO;
using DAL.Contracts.App;
using DAL.Contracts.Base;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BLL.Contracts.App;

public interface ICategoryService : IBaseRepository<CategoryBLLDTO>
{
    
}
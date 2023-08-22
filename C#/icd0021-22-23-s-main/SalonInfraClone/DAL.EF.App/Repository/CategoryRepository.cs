using AutoMapper;
using DAL.Contracts.App;
using DAL.Contracts.Base;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository;

public class CategoryRepository : EFBaseRepository<Category,ApplicationDbContext>,
    ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public override async Task<IEnumerable<Category>> AllAsync()
    {
        var categories = await RepositoryDbSet
            .Include(c => c.Services)
            .ToListAsync();

        /*var categoryAndServices = categories.Select(c => new Category()
        {
            Id = c.Id,
            CategoryName = c.CategoryName,
            CategoryImageUrl = c.CategoryImageUrl,
            Services = c.Services?.Select(s => new Service()
            {
                Id = s.Id,
                CategoryId = s.CategoryId,
                DiscountId = s.DiscountId,
                ServiceName = s.ServiceName,
                ServiceDuration = s.ServiceDuration,
                ServicePrice = s.ServicePrice
            }).ToList()
        });*/
        



        return categories;
    }
}
using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO;
using Contracts.Base;
using DAL.Contracts.App;
using Domain.App;

namespace BLL.App.Services;

public class CompanyService : BaseEntityService<CompanyBLLDTO,Company, ICompanyRepository>, ICompanyService
{
    protected IAppUOW Uow;
    public CompanyService(IAppUOW uow, IMapper<CompanyBLLDTO, Company> mapper) : base(uow.CompanyRepository, mapper)
    {
        Uow = uow;
    }
}
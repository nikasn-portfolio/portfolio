using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;

namespace DAL.Repository;

public class PaymentMethodRepository : EFBaseRepository<PaymentMethod,ApplicationDbContext>, IPaymentMethodRepository
{
    public PaymentMethodRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
        
    }
}
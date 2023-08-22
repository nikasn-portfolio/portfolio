using BLL.DTO;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IPaymentMethodService : IBaseRepository<PaymentMethodBLLDTO>
{
    
}
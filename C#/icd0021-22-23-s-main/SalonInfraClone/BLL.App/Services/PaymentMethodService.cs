using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO;
using Contracts.Base;
using DAL.Contracts.App;
using Domain.App;

namespace BLL.App.Services;

public class PaymentMethodService : BaseEntityService<PaymentMethodBLLDTO,PaymentMethod, IPaymentMethodRepository>, IPaymentMethodService
{
    protected IAppUOW Uow;
    public PaymentMethodService(IAppUOW uow, IMapper<PaymentMethodBLLDTO, PaymentMethod> mapper) : base(uow.PaymentMethodRepository, mapper)
    {
        Uow = uow;
    }
}
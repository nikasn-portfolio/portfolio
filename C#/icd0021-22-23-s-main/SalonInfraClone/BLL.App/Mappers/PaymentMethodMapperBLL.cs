using AutoMapper;
using BLL.DTO;
using DAL.Base;
using Domain.App;

namespace BLL.App.Mappers;

public class PaymentMethodMapperBLL : BaseMapper<PaymentMethodBLLDTO, PaymentMethod>
{
    public PaymentMethodMapperBLL(IMapper mapper) : base(mapper)
    {
    }
}
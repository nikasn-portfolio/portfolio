using AutoMapper;
using BLL.DTO;
using DAL.Base;
using Domain.App;

namespace Public.DTO.Mappers;

public class PaymentMethodMapper : BaseMapper<PaymentMethodDTO, PaymentMethodBLLDTO>
{
    public PaymentMethodMapper(IMapper mapper) : base(mapper)
    {
    }
}
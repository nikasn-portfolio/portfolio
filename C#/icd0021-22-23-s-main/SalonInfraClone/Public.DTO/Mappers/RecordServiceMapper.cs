using AutoMapper;
using BLL.DTO;
using DAL.Base;
using Domain.App;

namespace Public.DTO.Mappers;

public class RecordServiceMapper : BaseMapper<RecordServiceBLLDTO, RecordService>
{
    public RecordServiceMapper(IMapper mapper) : base(mapper)
    {
    }
}
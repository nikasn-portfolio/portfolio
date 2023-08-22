using AutoMapper;
using BLL.DTO;
using DAL.Base;
using Domain.App;
using Public.DTO.v1;

namespace Public.DTO.Mappers;

public class RecordMapper : BaseMapper<RecordDTO, RecordBLLDTO>
{
    public RecordMapper(IMapper mapper) : base(mapper)
    {
    }
}


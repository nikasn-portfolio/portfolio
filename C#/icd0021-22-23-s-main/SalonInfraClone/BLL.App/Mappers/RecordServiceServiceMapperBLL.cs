using AutoMapper;
using BLL.DTO;
using DAL.Base;
using Domain.App;

namespace BLL.App.Mappers;

public class RecordServiceServiceMapperBLL : BaseMapper<RecordServiceBLLDTO, RecordService>
{
    public RecordServiceServiceMapperBLL(IMapper mapper) : base(mapper)
    {
    }
}
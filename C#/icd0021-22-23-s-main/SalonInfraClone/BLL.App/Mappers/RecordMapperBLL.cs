using AutoMapper;
using BLL.DTO;
using DAL.Base;
using Domain.App;

namespace BLL.App.Mappers;

public class RecordMapperBLL : BaseMapper<RecordBLLDTO, Record>
{
    public RecordMapperBLL(IMapper mapper) : base(mapper)
    {
    }
}
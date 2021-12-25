using AutoMapper;
using Template.Core;

namespace Template.Application.Mapper;

public class DemoDtoProfile : Profile
{
    public DemoDtoProfile()
    {
        CreateMap<DemoDto, DemoVo>().ReverseMap();
    }
}
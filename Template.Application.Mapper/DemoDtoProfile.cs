using AutoMapper;
using Template.Core.Share;

namespace Template.Application.Share.Mapper;

public class DemoDtoProfile : Profile
{
    public DemoDtoProfile()
    {
        CreateMap<DemoPo, DemoDto>().ReverseMap();
    }
}
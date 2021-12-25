using AutoMapper;
using Template.Entity;

namespace Template.Core.Share.Mapper;

public class DemoPoProfile : Profile
{
    public DemoPoProfile()
    {
        CreateMap<Demo, DemoPo>().ReverseMap();
    }
}
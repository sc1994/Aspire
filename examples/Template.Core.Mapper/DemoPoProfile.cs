using AutoMapper;
using Template.Entity;

namespace Template.Core.Mapper;

public class DemoPoProfile : Profile
{
    public DemoPoProfile()
    {
        CreateMap<Demo, DemoDto>().ReverseMap();
    }
}
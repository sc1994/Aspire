using AutoMapper;
using Template.Entity;

namespace Template.Core.Share.Mapper;

public class DemoProfile : Profile
{
    public DemoProfile()
    {
        CreateMap<Demo, DemoPo>().ReverseMap();
    }
}
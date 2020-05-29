using Aspire.Test.Map;
using AutoMapper;
using System;

namespace Aspire.AutoMapper.Test
{
    public class Profile_Test : Profile
    {
        public Profile_Test()
        {
            CreateMap<DateTime, string>().ConvertUsing(x => x.ToString("yyyy-MM-dd HH:mm:ss"));
            CreateMap<GenderEnum, int>().ConvertUsing(x => x.GetHashCode());

            CreateMap<MapEntity_Test, MapDto_Test>()
                .ForMember(x => x.Birthday, x => x.MapFrom(o => o.DateOfBirth))
                .ForMember(x => x.Name, x => x.Condition(o => o.Name != default));
        }
    }
}

using Aspire.Json;

using AutoMapper;

using Demo.Core.Blogs;

namespace Demo.Application.Blogs.Dtos
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<BlogOutputDto, BlogEntity>()
                .ForMember(x => x.Directories, x => x.MapFrom(m => m.Directories.Serialize()))
                .ReverseMap()
                .ForMember(x => x.Directories, x => x.MapFrom(m => m.Directories.Deserialize<string[]>()));
        }
    }
}

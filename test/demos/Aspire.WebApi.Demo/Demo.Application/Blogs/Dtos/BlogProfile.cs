using AutoMapper;

using Demo.Core.Blogs;

namespace Demo.Application.Blogs.Dtos
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<BlogDto, BlogEntity>()
                .IgnoreCommonDto()
                .ReverseMap();
        }
    }
}

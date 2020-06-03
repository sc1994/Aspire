using AutoMapper;

using Demo.Core.Blogs;

namespace Demo.Application.Blogs.Dtos
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<BlogDto, BlogEntity>()
                .ForMember(x => x.CreatedAt, x => x.Ignore())
                .ForMember(x => x.DeleteAt, x => x.Ignore())
                .ForMember(x => x.UpdatedAt, x => x.Ignore());
            CreateMap<BlogEntity, BlogDto>();
        }
    }
}

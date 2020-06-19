
using Aspire.Application.AppServices.Dtos;

namespace Demo.Application.Blogs
{
    public class BlogOutputDto : OutputDto, IBlogDto
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string[] Directories { get; set; }
        public string Text { get; set; }
    }
}

using Aspire.Application.AppServices.Dtos;

namespace Demo.Application.Blogs
{
    public class BlogUpdateDto : UpdateDto, IBlogDto
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string[] Directories { get; set; }
        public string Text { get; set; }
    }
}
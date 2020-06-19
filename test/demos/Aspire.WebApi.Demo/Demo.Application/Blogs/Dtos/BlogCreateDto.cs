using Aspire.DynamicForm;

namespace Demo.Application.Blogs
{
    public class BlogCreateDto : IBlogDto
    {
        [InputProps("标题")]
        public string Title { get; set; }
        [InputProps("副标题")]
        public string Subtitle { get; set; }
        [SelectProps("博客分类")]
        [SelectOption()]
        public int BlogType { get; set; }
        [InputProps("内容", Maxlength = 100, Type = "textarea")]
        [InputTextareaAutosize(20, 50)]
        public string Text { get; set; } = "详细说说吧";
    }
}
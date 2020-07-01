namespace Demo.Application.Blogs
{
    public class BlogCreateDto : IBlogDto
    {
        //[InputProps("标题")]
        //[Validate(Required = true, Message = "标题必填")]
        //[Validate(Max = 20, Message = "标题内容不能超过20个字符")]
        public string Title { get; set; }

        //[InputProps("副标题")]
        //[Validate(Required = true, Message = "副标题必填")]
        public string Subtitle { get; set; }

        //[SelectProps("博客分类", Placeholder = "请选择博客分类")]
        //[SelectOptions(".net", "dotnet")]
        //[SelectOptions("java")]
        //[Validate(Required = true, Message = "博客分类必选")]
        public int BlogType { get; set; }

        //[SelectProps("子类")]
        //[SelectOptions(typeof(BlogSubType))]
        public int BlogSubType { get; set; }

        //[InputProps("内容", Maxlength = 100, Type = "textarea")]
        //[InputTextareaAutosize(20, 50)]
        //[Validate(Required = true, Message = "请填写内容")]
        //[Validate(Min = 10, Message = "内容字数不能少于10个字符")]
        public string Text { get; set; } = "详细说说吧";
    }

    public enum BlogSubType
    {
        Mvc,
        Api,
        Log
    }
}
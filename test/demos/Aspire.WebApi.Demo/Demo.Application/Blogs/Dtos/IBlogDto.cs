namespace Demo.Application.Blogs
{
    public interface IBlogDto
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 副标题
        /// </summary>
        public string Subtitle { get; set; }

        /// <summary>
        /// 文本
        /// </summary>
        public string Text { get; set; }
    }
}

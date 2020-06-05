using System;

using Aspire.Application.AppServices.Dtos;

namespace Demo.Application.Blogs
{
    public class BlogDto : CommonDto
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
        /// 目录
        /// </summary>
        public string[] Directories { get; set; }

        /// <summary>
        /// 文本
        /// </summary>
        public string Text { get; set; }
    }
}

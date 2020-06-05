
using System.ComponentModel.DataAnnotations;

using Aspire.Domain.Entities;

namespace Demo.Core.Blogs
{
    public class BlogEntity : BaseEfCoreEntity<long>
    {
        /// <summary>
        /// 标题
        /// </summary>
        [MaxLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// 副标题
        /// </summary>
        [MaxLength(100)]
        public string Subtitle { get; set; }

        /// <summary>
        /// 目录
        /// </summary>
        [MaxLength(500)]
        public string Directories { get; set; }

        /// <summary>
        /// 文本
        /// </summary>
        public string Text { get; set; }
    }
}

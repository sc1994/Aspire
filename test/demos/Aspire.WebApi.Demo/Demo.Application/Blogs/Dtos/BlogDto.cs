using System;

namespace Demo.Application.Blogs
{
    public class BlogDto
    {
        public long Id { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime UpdateAt { get; set; }

        public DateTime DeleteAt { get; set; }
    }
}

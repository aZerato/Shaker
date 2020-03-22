using System;

namespace shaker.Models.Posts
{
    public class PostListModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string Description { get; set; }

        public DateTime Creation { get; set; }
    }
}

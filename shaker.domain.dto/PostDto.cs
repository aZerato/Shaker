using System;

namespace shaker.domain.dto
{
    public class PostDto : IBaseDto
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string Description { get; set; }

        public DateTime Creation { get; set;  }
    }
}

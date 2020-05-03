using System;

namespace shaker.domain.dto
{
    public class PostDto : IBaseDto
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public string Description { get; set; }

        public DateTime Creation { get; set; }

        public string Error { get; set; }
    }
}

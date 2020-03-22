using System;
using shaker.data.core;

namespace shaker.data.entity
{
    public class Post : IBaseEntity
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public DateTime Creation { get; set; }
    }
}

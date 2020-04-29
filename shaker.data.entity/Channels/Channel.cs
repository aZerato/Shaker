using System;
using shaker.data.core;

namespace shaker.data.entity.Channels
{
    public class Channel : IBaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImgPath { get; set; }

        public DateTime Creation { get; set; }
    }
}
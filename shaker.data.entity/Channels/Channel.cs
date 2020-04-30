using System;
using System.Collections.Generic;
using shaker.data.core;
using shaker.data.entity.Users;

namespace shaker.data.entity.Channels
{
    public class Channel : IBaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImgPath { get; set; }

        public DateTime Creation { get; set; }

        public IList<User> Users { get; set; }
    }
}
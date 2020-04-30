using System;
using shaker.data.core;
using shaker.data.entity.Users;

namespace shaker.data.entity.Channels
{
    public class Message : IBaseEntity
    {
        public int Id { get; set; }

        public User User { get; set; }

        public Channel Channel { get; set; }

        public string Content { get; set; }

        public DateTime Creation { get; set; }
    }
}
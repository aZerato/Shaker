using System;
using shaker.data.core;

namespace shaker.data.entity.Channels
{
    public class Message : IBaseEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ChannelId { get; set; }

        public string Content { get; set; }

        public DateTime Creation { get; set; }
    }
}
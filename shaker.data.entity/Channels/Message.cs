using System;
using LiteDB;
using shaker.data.core;
using shaker.data.entity.Users;

namespace shaker.data.entity.Channels
{
    public class Message : IBaseEntity
    {
        public Message()
        {
            Id = ObjectId.NewObjectId().ToString();
        }

        [BsonId]
        public string Id { get; set; }

        [BsonRef("User")]
        public User User { get; set; }

        [BsonRef("Channel")]
        public Channel Channel { get; set; }

        public string Content { get; set; }

        public DateTime Creation { get; set; }
    }
}
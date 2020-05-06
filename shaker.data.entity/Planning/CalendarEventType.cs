using System;
using LiteDB;
using shaker.data.core;

namespace shaker.data.entity.Planning
{
    public class CalendarEventType : IBaseEntity
    {
        public CalendarEventType()
        {
            Id = ObjectId.NewObjectId().ToString();
        }

        [BsonId]
        public string Id { get; set; }

        public string Title { get; set; }
    }
}
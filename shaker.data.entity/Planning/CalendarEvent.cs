using System;
using LiteDB;
using shaker.data.core;
using shaker.data.entity.Users;

namespace shaker.data.entity.Planning
{
    public class CalendarEvent : IBaseEntity
    {
        public CalendarEvent()
        {
            Id = ObjectId.NewObjectId().ToString();
        }

        [BsonId]
        public string Id { get; set; }

        public DateTime Start { get; set; }

        public DateTime? End { get; set; }

        public string Title { get; set; }

        [BsonRef("User")]
        public User User { get; set; }

        [BsonRef("CalendarEventType")]
        public CalendarEventType Type  { get; set; }

        public string hexColor { get; set; }

        public bool AllDay { get; set; }
    }
}
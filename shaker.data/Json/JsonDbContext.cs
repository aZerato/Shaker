using System;
using LiteDB;
using shaker.data.core;
using shaker.data.entity.Posts;
using shaker.data.entity.Channels;
using shaker.data.entity.Users;
using shaker.data.entity.Movements;
using shaker.data.entity.Planning;

namespace shaker.data.Json
{
    public class JsonDbContext : IDbContext
    {
        private readonly ILiteDatabase _liteDatabase;

        public JsonDbContext(ILiteDatabase liteDatabase)
        {
            _liteDatabase = liteDatabase;

            Users = new JsonDbSet<User>(_liteDatabase);
            Roles = new JsonDbSet<Role>(_liteDatabase);

            Posts = new JsonDbSet<Post>(_liteDatabase);
            Messages = new JsonDbSet<Message>(_liteDatabase);
            Channels = new JsonDbSet<Channel>(_liteDatabase);

            BodyParts = new JsonDbSet<BodyPart>(_liteDatabase);
            BodyZones = new JsonDbSet<BodyZone>(_liteDatabase);
            Movements = new JsonDbSet<Movement>(_liteDatabase);
            MovementBodyZones = new JsonDbSet<MovementBodyZone>(_liteDatabase);
            MovementBodyParts = new JsonDbSet<MovementBodyPart>(_liteDatabase);
            MovementTypes = new JsonDbSet<MovementType>(_liteDatabase);

            CalendarEvents = new JsonDbSet<CalendarEvent>(_liteDatabase);
            CalendarEventTypes = new JsonDbSet<CalendarEventType>(_liteDatabase);
        }

        public IDbSet<User> Users { get; }
        public IDbSet<Role> Roles { get; }

        public IDbSet<Post> Posts { get; }
        public IDbSet<Message> Messages { get; }
        public IDbSet<Channel> Channels { get; }

        public IDbSet<BodyPart> BodyParts { get; }
        public IDbSet<BodyZone> BodyZones { get; }
        public IDbSet<Movement> Movements { get; }
        public IDbSet<MovementBodyZone> MovementBodyZones { get; }
        public IDbSet<MovementBodyPart> MovementBodyParts { get; }
        public IDbSet<MovementType> MovementTypes { get; }

        public IDbSet<CalendarEvent> CalendarEvents { get; }
        public IDbSet<CalendarEventType> CalendarEventTypes { get; }

        public void Commit()
        {
            _liteDatabase.Commit();
        }

        public void RollbackChanges()
        {
            _liteDatabase.Rollback();
        }


        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _liteDatabase.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

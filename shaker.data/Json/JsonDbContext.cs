using System;
using LiteDB;
using shaker.data.core;
using shaker.data.entity.Posts;
using shaker.data.entity.Channels;
using shaker.data.entity.Users;
using shaker.data.entity.Movements;

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
            MovementBodyZone = new JsonDbSet<MovementBodyZone>(_liteDatabase);
            MovementBodyPart = new JsonDbSet<MovementBodyPart>(_liteDatabase);
            MovementTypes = new JsonDbSet<MovementType>(_liteDatabase);
        }

        public IDbSet<User> Users { get; }
        public IDbSet<Role> Roles { get; }

        public IDbSet<Post> Posts { get; }
        public IDbSet<Message> Messages { get; }
        public IDbSet<Channel> Channels { get; }

        public IDbSet<BodyPart> BodyParts { get; }
        public IDbSet<BodyZone> BodyZones { get; }
        public IDbSet<Movement> Movements { get; }
        public IDbSet<MovementBodyZone> MovementBodyZone { get; }
        public IDbSet<MovementBodyPart> MovementBodyPart { get; }
        public IDbSet<MovementType> MovementTypes { get; }

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

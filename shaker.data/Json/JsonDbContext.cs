using System;
using LiteDB;
using shaker.data.core;
using shaker.data.entity;
using shaker.data.entity.Channels;
using shaker.data.entity.Users;

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
        }

        public IDbSet<User> Users { get; }
        public IDbSet<Role> Roles { get; }

        public IDbSet<Post> Posts { get; }
        public IDbSet<Message> Messages { get; }
        public IDbSet<Channel> Channels { get; }

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

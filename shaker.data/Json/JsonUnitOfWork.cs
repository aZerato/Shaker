using System;
using shaker.data.core;
using shaker.data.entity;
using shaker.data.entity.Channels;
using shaker.data.entity.Users;

namespace shaker.data.Json
{
    public class JsonUnitOfWork : IUnitOfWork
    {
        private readonly IDbContext _jsonDbContext;

        public JsonUnitOfWork(IDbContext jsonDbContext)
        {
            _jsonDbContext = jsonDbContext;

            Users = new Repository<User>(jsonDbContext.Users);
            Roles = new Repository<Role>(jsonDbContext.Roles);

            Posts = new Repository<Post>(jsonDbContext.Posts);
            Messages = new Repository<Message>(jsonDbContext.Messages);
            Channels = new Repository<Channel>(jsonDbContext.Channels);
        }

        public IRepository<User> Users { get; }
        public IRepository<Role> Roles { get; }

        public IRepository<Post> Posts { get; }
        public IRepository<Message> Messages { get; }
        public IRepository<Channel> Channels { get; }

        public void Commit()
        {
            _jsonDbContext.Commit();
        }

        public void RollbackChanges()
        {
            _jsonDbContext.RollbackChanges();
        }


        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _jsonDbContext.Dispose();
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

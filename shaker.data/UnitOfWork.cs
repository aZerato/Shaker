using System;
using shaker.data.core;
using shaker.data.entity.Posts;
using shaker.data.entity.Channels;
using shaker.data.entity.Users;
using shaker.data.entity.Movements;
using shaker.data.entity.Planning;

namespace shaker.data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContext _dbContext;

        public UnitOfWork(IDbContext dbContext)
        {
            _dbContext = dbContext;

            Users = new Repository<User>(dbContext.Users);
            Roles = new Repository<Role>(dbContext.Roles);

            Posts = new Repository<Post>(dbContext.Posts);
            Messages = new Repository<Message>(dbContext.Messages);
            Channels = new Repository<Channel>(dbContext.Channels);

            BodyParts = new Repository<BodyPart>(dbContext.BodyParts);
            BodyZones = new Repository<BodyZone>(dbContext.BodyZones);
            Movements = new Repository<Movement>(dbContext.Movements);
            MovementBodyZones = new Repository<MovementBodyZone>(dbContext.MovementBodyZones);
            MovementBodyParts = new Repository<MovementBodyPart>(dbContext.MovementBodyParts);
            MovementTypes = new Repository<MovementType>(dbContext.MovementTypes);

            CalendarEvents = new Repository<CalendarEvent>(dbContext.CalendarEvents);
            CalendarEventTypes = new Repository<CalendarEventType>(dbContext.CalendarEventTypes);
        }

        public IRepository<User> Users { get; }
        public IRepository<Role> Roles { get; }

        public IRepository<Post> Posts { get; }
        public IRepository<Message> Messages { get; }
        public IRepository<Channel> Channels { get; }

        public IRepository<BodyPart> BodyParts { get; }
        public IRepository<BodyZone> BodyZones { get; }
        public IRepository<Movement> Movements { get; }
        public IRepository<MovementBodyZone> MovementBodyZones { get; }
        public IRepository<MovementBodyPart> MovementBodyParts { get; }
        public IRepository<MovementType> MovementTypes { get; }

        public IRepository<CalendarEvent> CalendarEvents { get; }
        public IRepository<CalendarEventType> CalendarEventTypes { get; }

        public void Commit()
        {
            _dbContext.Commit();
        }

        public void RollbackChanges()
        {
            _dbContext.RollbackChanges();
        }


        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
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

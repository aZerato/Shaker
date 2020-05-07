using System;
using shaker.data.core;
using shaker.data.entity.Posts;
using shaker.data.entity.Channels;
using shaker.data.entity.Users;
using shaker.data.entity.Movements;
using shaker.data.entity.Planning;

namespace shaker.data
{
    public interface IDbContext : IDisposable
    {
        void Commit();
        void RollbackChanges();

        IDbSet<User> Users { get; }
        IDbSet<Role> Roles { get; }

        IDbSet<Post> Posts { get; }

        IDbSet<Message> Messages { get; }
        IDbSet<Channel> Channels { get; }

        IDbSet<BodyPart> BodyParts { get; }
        IDbSet<BodyZone> BodyZones { get; }
        IDbSet<Movement> Movements { get; }
        IDbSet<MovementBodyZone> MovementBodyZones { get; }
        IDbSet<MovementBodyPart> MovementBodyParts { get; }
        IDbSet<MovementType> MovementTypes { get; }

        IDbSet<CalendarEvent> CalendarEvents { get; }
        IDbSet<CalendarEventType> CalendarEventTypes { get; }
    }
}

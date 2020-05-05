using System;
using shaker.data.core;
using shaker.data.entity;
using shaker.data.entity.Channels;
using shaker.data.entity.Users;

namespace shaker.data
{
    public interface IDbContext : IDisposable
    {
        IDbSet<User> Users { get; }
        IDbSet<Role> Roles { get; }

        IDbSet<Post> Posts { get; }
        IDbSet<Message> Messages { get; }
        IDbSet<Channel> Channels { get; }

        void Commit();
        void RollbackChanges();
    }
}

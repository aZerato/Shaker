﻿using System;
using shaker.data.core;
using shaker.data.entity.Posts;
using shaker.data.entity.Channels;
using shaker.data.entity.Users;
using shaker.data.entity.Movements;

namespace shaker.data
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();

        virtual void CommitAndRefreshChanges() { }

        void RollbackChanges();

        IRepository<User> Users { get; }
        IRepository<Role> Roles { get; }

        IRepository<Post> Posts { get; }

        IRepository<Message> Messages { get; }
        IRepository<Channel> Channels { get; }

        IRepository<BodyPart> BodyParts { get; }
        IRepository<BodyZone> BodyZones { get; }
        IRepository<Movement> Movements { get; }
        IRepository<MovementBodyZone> MovementBodyZone { get; }
        IRepository<MovementBodyPart> MovementBodyPart { get; }
        IRepository<MovementType> MovementTypes { get; }
    }
}

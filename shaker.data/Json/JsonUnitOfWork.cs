﻿using LiteDB;
using shaker.data.core;
using shaker.data.entity;
using shaker.data.entity.Channels;
using shaker.data.entity.Users;

namespace shaker.data.Json
{
    public class JsonUnitOfWork : IUnitOfWork
    {
        private readonly LiteDatabase _liteDatabase;

        public JsonUnitOfWork(string path)
        {
            _liteDatabase = new LiteDatabase(path);
        }

        #region ---- properties ----

        public JsonDbSet<User> Users { get; set; }
        public JsonDbSet<Role> Roles { get; set; }

        public JsonDbSet<Post> Posts { get; set; }
        public JsonDbSet<Message> Messages { get; set; }
        public JsonDbSet<Channel> Channels { get; set; }
        

        #endregion

        public void Commit()
        {
            _liteDatabase.Commit();
        }

        public IDbSet<TEntity> CreateSet<TEntity>()
            where TEntity : IBaseEntity
        {
            return new JsonDbSet<TEntity>(_liteDatabase);
        }

        public void Dispose()
        {
            _liteDatabase.Dispose();
        }

        public void RollbackChanges()
        {
            _liteDatabase.Rollback();
        }
    }
}

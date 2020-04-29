using System;
using LiteDB;
using shaker.data.core;
using shaker.data.entity;
using shaker.data.entity.Channels;

namespace shaker.data.Json
{
    public class JsonUnitOfWork : IUnitOfWork
    {
        private LiteDatabase _liteDatabase;

        public JsonUnitOfWork(string jsonBdpath)
        {
            _liteDatabase = new LiteDatabase(jsonBdpath);
        }

        #region ---- properties ----

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

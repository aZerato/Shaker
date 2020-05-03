using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LiteDB;
using shaker.data.core;

namespace shaker.data.Json
{
    public class JsonDbSet<TEntity> : IDbSet<TEntity>
        where TEntity : IBaseEntity
    {
        private readonly ILiteDatabase _liteDatabase;
        private readonly ILiteCollection<TEntity> _collection;

        public JsonDbSet(ILiteDatabase liteDatabase)
        {
            _liteDatabase = liteDatabase;
            _collection = _liteDatabase.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public bool EnsureUniqueIndex(string propertyName)
        {
            return _collection.EnsureIndex(propertyName, true);
        }

        public string Add(TEntity entity)
        {
            return _collection.Insert(entity);
        }

        public void Attach(TEntity entity)
        {
            _collection.Query().ForUpdate();
        }

        public bool Update(TEntity entity)
        {
            return _collection.Update(entity);
        }

        public bool Remove(TEntity entity)
        {
            return _collection.Delete(entity.Id);
        }

        public TEntity Find(string id)
        {
            return _collection.FindById(id);
        }

        public IEnumerable<TEntity> AsEnumerable()
        {
            return _collection.Query().ToEnumerable();
        }

        public IEnumerable<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> selectBuilder)
            where TResult : IBaseEntity
        {
            return _collection.Query().Select(selectBuilder).ToEnumerable();
        }

        public IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return _collection.Query().Where(predicate).ToEnumerable();
        }

        public IEnumerable<TResult> Where<TResult>(Expression<Func<TEntity, TResult>> selectBuilder,
            Expression<Func<TEntity, bool>> predicate)
            where TResult : IBaseEntity
        {
            return _collection.Query().Where(predicate).Select(selectBuilder).ToEnumerable();
        }
    }
}

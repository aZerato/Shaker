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

        private ILiteCollection<TEntity> _collection;

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

        public TEntity Find(string id, params Expression<Func<TEntity, IBaseEntity>>[] includes)
        {
            return Includes(_collection, includes).FindById(id);
        }

        public IEnumerable<TEntity> AsEnumerable(params Expression<Func<TEntity, IBaseEntity>>[] includes)
        {
            return Includes(_collection, includes).Query().ToEnumerable();
        }

        public IEnumerable<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> selectBuilder,
            params Expression<Func<TEntity, IBaseEntity>>[] includes)
            where TResult : IBaseEntity
        {
            return Includes(_collection, includes).Query().Select(selectBuilder).ToEnumerable();
        }

        public IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, IBaseEntity>>[] includes)
        {
            return Includes(_collection, includes).Query().Where(predicate).ToEnumerable();
        }

        public IEnumerable<TResult> Where<TResult>(Expression<Func<TEntity, TResult>> selectBuilder,
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, IBaseEntity>>[] includes)
            where TResult : IBaseEntity
        {
            return Includes(_collection, includes).Query().Where(predicate).Select(selectBuilder).ToEnumerable();
        }

        private ILiteCollection<TEntity> Includes(ILiteCollection<TEntity> collection,
            params Expression<Func<TEntity, IBaseEntity>>[] includes)
        {
            if (includes == null) return collection;

            foreach(Expression<Func<TEntity, IBaseEntity>> include in includes)
            {
                collection = collection.Include(include);
            }

            return collection;
        }
    }
}

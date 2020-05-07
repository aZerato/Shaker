using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace shaker.data.core
{
    public interface IDbSet<TEntity>
        where TEntity : IBaseEntity
    {
        bool EnsureUniqueIndex(string propertyName);

        string Add(TEntity entity);

        void Attach(TEntity entity);

        bool Update(TEntity entity);

        bool Remove(TEntity entity);

        TEntity Find(string id, params Expression<Func<TEntity, IBaseEntity>>[] includes);

        IEnumerable<TEntity> AsEnumerable(params Expression<Func<TEntity, IBaseEntity>>[] includes);

        IEnumerable<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> selectBuilder,
            params Expression<Func<TEntity, IBaseEntity>>[] includes)
            where TResult : IBaseEntity;

        IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, IBaseEntity>>[] includes);

        IEnumerable<TResult> Where<TResult>(Expression<Func<TEntity, TResult>> selectBuilder,
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, IBaseEntity>>[] includes)
            where TResult : IBaseEntity;
    }
}
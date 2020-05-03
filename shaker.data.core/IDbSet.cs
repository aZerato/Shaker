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

        TEntity Find(string id);

        IEnumerable<TEntity> AsEnumerable();

        IEnumerable<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> selectBuilder)
            where TResult : IBaseEntity;

        IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TResult> Where<TResult>(Expression<Func<TEntity, TResult>> selectBuilder,
            Expression<Func<TEntity, bool>> predicate)
            where TResult : IBaseEntity;
    }
}
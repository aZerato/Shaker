﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace shaker.data.core
{
    public interface IDbSet<TEntity>
        where TEntity : IBaseEntity
    {
        int Add(TEntity entity);

        void Attach(TEntity entity);

        void Remove(TEntity entity);

        TEntity Find(int id);

        IEnumerable<TEntity> AsEnumerable();

        IEnumerable<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> selectBuilder)
            where TResult : IBaseEntity;

        IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TResult> Where<TResult>(Expression<Func<TEntity, TResult>> selectBuilder,
            Expression<Func<TEntity, bool>> predicate)
            where TResult : IBaseEntity;
    }
}
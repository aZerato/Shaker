﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace shaker.data.core
{
    /// <summary>
    /// The Repository Implementation.
    /// </summary>
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : IBaseEntity
    {
        #region ---- Fields ----

        /// <summary>
        /// The current UoW.
        /// </summary>
        private readonly IUnitOfWork _currentUoW;

        #endregion

        #region ---- Constructor ----

        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="unitOfWork"></param>
        public Repository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("UoW");
            }

            _currentUoW = unitOfWork;
        }

        #endregion

        #region ---- Repository Implementation ----

        /// <summary>
        /// <see cref="IRepository{TEntity}.Add(TEntity)"/> 
        /// </summary>
        /// <param name="entity"><see cref="IRepository{TEntity}.Add(TEntity)"/> </param>
        public virtual int Add(TEntity entity)
        {
            return _currentUoW.CreateSet<TEntity>().Add(entity);
        }

        /// <summary>
        /// <see cref="IRepository{TEntity}.Remove(TEntity)"/>
        /// </summary>
        /// <param name="entity"><see cref="IRepository{TEntity}.Remove(TEntity)"/></param>
        public virtual bool Remove(TEntity entity)
        {
            bool state = false;
            using (var uow = _currentUoW)
            {
                var oSet = uow.CreateSet<TEntity>();

                oSet.Attach(entity);
                state = oSet.Remove(entity);
            }
            return state;
        }

        /// <summary>
        /// <see cref="IRepository{TEntity}.Update(TEntity)"/>
        /// </summary>
        /// <param name="entity"><see cref="IRepository{TEntity}.Update(TEntity)"/></param>
        public virtual bool Update(TEntity entity)
        {
            bool state = false;
            using (var uow = _currentUoW)
            {
                var oSet = uow.CreateSet<TEntity>();

                oSet.Attach(entity);
                state = oSet.Update(entity);
            }
            return state;
        }

        /// <summary>
        /// <see cref="IRepository{TEntity}.Get(int)"/>
        /// </summary>
        /// <param name="id"><see cref="IRepository{TEntity}.Get(int)"/></param>
        /// <returns></returns>
        public virtual TEntity Get(int id)
        {
            return _currentUoW.CreateSet<TEntity>().Find(id);
        }

        /// <summary>
        /// <see cref="IRepository{TEntity}.GetAll()"/>
        /// </summary>
        /// <returns><see cref="IRepository{TEntity}.GetAll()"/></returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return _currentUoW.CreateSet<TEntity>().AsEnumerable();
        }

        /// <summary>
        /// <see cref="IRepository{TEntity}.GetAll(Expression, Expression)"/>
        /// </summary>
        /// <typeparam name="TResult"><see cref="IRepository{TEntity}.GetAll(Expression, Expression)"/></typeparam>
        /// <param name="selectBuilder"><see cref="IRepository{TEntity}.GetAll(Expression, Expression)"/></param>
        /// <returns><see cref="IRepository{TEntity}.GetAllAndCount(Expression, Expression)"/></returns>
        public virtual IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selectBuilder)
                where TResult : IBaseEntity
        {
            return _currentUoW.CreateSet<TEntity>().Select(selectBuilder).AsEnumerable();
        }

        /// <summary>
        /// <see cref="IRepository{TEntity}.GetAll(Expression)"/>
        /// </summary>
        /// <typeparam name="TResult"><see cref="IRepository{TEntity}.GetAll(Expression)"/></typeparam>
        /// <param name="predicate"><see cref="IRepository{TEntity}.GetAll(Expression)"/></param>
        /// <returns><see cref="IRepository{TEntity}.GetAllAndCount(Expression)"/></returns>
        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return _currentUoW.CreateSet<TEntity>().Where(predicate);
        }

        /// <summary>
        /// <see cref="IRepository{TEntity}.GetAll(Expression, Expression)"/>
        /// </summary>
        /// <typeparam name="TResult"><see cref="IRepository{TEntity}.GetAll(Expression, Expression)"/></typeparam>
        /// <param name="selectBuilder"><see cref="IRepository{TEntity}.GetAll(Expression, Expression)"/></param>
        /// <param name="predicate"><see cref="IRepository{TEntity}.GetAll(Expression, Expression)"/></param>
        /// <returns><see cref="IRepository{TEntity}.GetAllAndCount(Expression, Expression)"/></returns>
        public virtual IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selectBuilder,
            Expression<Func<TEntity, bool>> predicate)
                where TResult : IBaseEntity
        {
            return _currentUoW.CreateSet<TEntity>().Where(selectBuilder, predicate);
        }

        /// <summary>
        /// <see cref="IRepository{TEntity}.GetAllAndCount(Expression, Expression)"/>
        /// </summary>
        /// <typeparam name="TResult"><see cref="IRepository{TEntity}.GetAllAndCount(Expression, Expression)"/></typeparam>
        /// <param name="selectBuilder"><see cref="IRepository{TEntity}.GetAllAndCount(Expression, Expression)"/></param>
        /// <param name="predicate"><see cref="IRepository{TEntity}.GetAllAndCount(Expression, Expression)"/></param>
        /// <returns><see cref="IRepository{TEntity}.GetAllAndCount(Expression, Expression)"/></returns>
        public virtual Tuple<IEnumerable<TResult>, int> GetAllAndCount<TResult>(
            Expression<Func<TEntity, TResult>> selectBuilder,
            Expression<Func<TEntity, bool>> predicate)
                where TResult : IBaseEntity
        {
            var oSet = _currentUoW.CreateSet<TEntity>();

            var filtered = oSet.Where(selectBuilder, predicate);
            var count = filtered.Count();

            return new Tuple<IEnumerable<TResult>, int>(filtered, count);
        }

        #endregion
    }
}

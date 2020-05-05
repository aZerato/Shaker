using System;
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
        /// The current DBSet.
        /// </summary>
        private readonly IDbSet<TEntity> _dbSet;

        #endregion

        #region ---- Constructor ----

        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="dbSet"></param>
        public Repository(IDbSet<TEntity> dbSet)
        {
            if (dbSet == null)
            {
                throw new ArgumentNullException("dbSet");
            }

            _dbSet = dbSet;
        }

        #endregion

        #region ---- Repository Implementation ----

        /// <summary>
        /// <see cref="IRepository{TEntity}.EnsureUniqueIndex(propertyName)"/> 
        /// </summary>
        /// <param name="propertyName"><see cref="IRepository{TEntity}.EnsureUniqueIndex(propertyName)"/> </param>
        public virtual bool EnsureUniqueIndex(string propertyName)
        {
            return _dbSet.EnsureUniqueIndex(propertyName);
        }

        /// <summary>
        /// <see cref="IRepository{TEntity}.Add(TEntity)"/> 
        /// </summary>
        /// <param name="entity"><see cref="IRepository{TEntity}.Add(TEntity)"/> </param>
        public virtual string Add(TEntity entity)
        {
            return _dbSet.Add(entity);
        }

        /// <summary>
        /// <see cref="IRepository{TEntity}.Remove(TEntity)"/>
        /// </summary>
        /// <param name="entity"><see cref="IRepository{TEntity}.Remove(TEntity)"/></param>
        public virtual bool Remove(TEntity entity)
        {
            return _dbSet.Remove(entity);
        }

        /// <summary>
        /// <see cref="IRepository{TEntity}.Update(TEntity)"/>
        /// </summary>
        /// <param name="entity"><see cref="IRepository{TEntity}.Update(TEntity)"/></param>
        public virtual bool Update(TEntity entity)
        {
            return _dbSet.Update(entity);
        }

        /// <summary>
        /// <see cref="IRepository{TEntity}.Get(int)"/>
        /// </summary>
        /// <param name="id"><see cref="IRepository{TEntity}.Get(int)"/></param>
        /// <returns></returns>
        public virtual TEntity Get(string id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// <see cref="IRepository{TEntity}.Get(Expression)"/>
        /// </summary>
        /// <typeparam name="TResult"><see cref="IRepository{TEntity}.Get(Expression)"/></typeparam>
        /// <param name="predicate"><see cref="IRepository{TEntity}.Get(Expression)"/></param>
        /// <returns><see cref="IRepository{TEntity}.Get(Expression)"/></returns>
        public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).SingleOrDefault();
        }

        /// <summary>
        /// <see cref="IRepository{TEntity}.GetAll()"/>
        /// </summary>
        /// <returns><see cref="IRepository{TEntity}.GetAll()"/></returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return _dbSet.AsEnumerable();
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
            return _dbSet.Select(selectBuilder);
        }

        /// <summary>
        /// <see cref="IRepository{TEntity}.GetAll(Expression)"/>
        /// </summary>
        /// <typeparam name="TResult"><see cref="IRepository{TEntity}.GetAll(Expression)"/></typeparam>
        /// <param name="predicate"><see cref="IRepository{TEntity}.GetAll(Expression)"/></param>
        /// <returns><see cref="IRepository{TEntity}.GetAllAndCount(Expression)"/></returns>
        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
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
            return _dbSet.Where(selectBuilder, predicate);
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
            IEnumerable<TResult> filtered = _dbSet.Where(selectBuilder, predicate);
            int count = filtered.Count();

            Tuple<IEnumerable<TResult>, int>  result = new Tuple<IEnumerable<TResult>, int>(filtered, count);

            return result;
        }

        #endregion
    }
}

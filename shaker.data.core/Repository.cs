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
        /// <see cref="IRepository{TEntity}.Get(int, Expression[])"/>
        /// </summary>
        /// <param name="id"><see cref="IRepository{TEntity}.Get(int, Expression[])"/></param>
        /// <param name="includes"><see cref="IRepository{TEntity}.Get(int, Expression[])"/></param>
        /// <returns></returns>
        public virtual TEntity Get(string id,
            params Expression<Func<TEntity, IBaseEntity>>[] includes)
        {
            return _dbSet.Find(id, includes);
        }

        /// <summary>
        /// <see cref="IRepository{TEntity}.Get(Expression, Expression[])"/>
        /// </summary>
        /// <typeparam name="TResult"><see cref="IRepository{TEntity}.Get(Expression, Expression[])"/></typeparam>
        /// <param name="predicate"><see cref="IRepository{TEntity}.Get(Expression, Expression[])"/></param>
        /// <param name="includes"><see cref="IRepository{TEntity}.Get(int, Expression[])"/></param>
        /// <returns><see cref="IRepository{TEntity}.Get(Expression, Expression[])"/></returns>
        public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, IBaseEntity>>[] includes)
        {
            return _dbSet.Where(predicate, includes).SingleOrDefault();
        }

        /// <summary>
        /// <see cref="IRepository{TEntity}.GetAll(Expression[])"/>
        /// </summary>
        /// <param name="includes"><see cref="IRepository{TEntity}.Get(int, Expression[])"/></param>
        /// <returns><see cref="IRepository{TEntity}.GetAll(Expression[])"/></returns>
        public virtual IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, IBaseEntity>>[] includes)
        {
            return _dbSet.AsEnumerable(includes);
        }

        /// <summary>
        /// <see cref="IRepository{TEntity}.GetAll(Expression, Expression, params Expression[])"/>
        /// </summary>
        /// <typeparam name="TResult"><see cref="IRepository{TEntity}.GetAll(Expression, Expression, params Expression[])"/></typeparam>
        /// <param name="selectBuilder"><see cref="IRepository{TEntity}.GetAll(Expression, Expression, params Expression[])"/></param>
        /// <param name="includes"><see cref="IRepository{TEntity}.Get(int, Expression[])"/></param>
        /// <returns><see cref="IRepository{TEntity}.GetAllAndCount(Expression, Expression, params Expression[])"/></returns>
        public virtual IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selectBuilder,
                params Expression<Func<TEntity, IBaseEntity>>[] includes)
                where TResult : IBaseEntity
        {
            return _dbSet.Select(selectBuilder, includes);
        }

        /// <summary>
        /// <see cref="IRepository{TEntity}.GetAll(Expression)"/>
        /// </summary>
        /// <typeparam name="TResult"><see cref="IRepository{TEntity}.GetAll(Expression)"/></typeparam>
        /// <param name="predicate"><see cref="IRepository{TEntity}.GetAll(Expression)"/></param>
        /// <param name="includes"><see cref="IRepository{TEntity}.Get(int, Expression[])"/></param>
        /// <returns><see cref="IRepository{TEntity}.GetAllAndCount(Expression)"/></returns>
        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, IBaseEntity>>[] includes)
        {
            return _dbSet.Where(predicate, includes);
        }

        /// <summary>
        /// <see cref="IRepository{TEntity}.GetAll(Expression, Expression)"/>
        /// </summary>
        /// <typeparam name="TResult"><see cref="IRepository{TEntity}.GetAll(Expression, Expression)"/></typeparam>
        /// <param name="selectBuilder"><see cref="IRepository{TEntity}.GetAll(Expression, Expression)"/></param>
        /// <param name="predicate"><see cref="IRepository{TEntity}.GetAll(Expression, Expression)"/></param>
        /// <param name="includes"><see cref="IRepository{TEntity}.Get(int, Expression[])"/></param>
        /// <returns><see cref="IRepository{TEntity}.GetAllAndCount(Expression, Expression)"/></returns>
        public virtual IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selectBuilder,
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, IBaseEntity>>[] includes)
                where TResult : IBaseEntity
        {
            return _dbSet.Where(selectBuilder, predicate, includes);
        }

        /// <summary>
        /// <see cref="IRepository{TEntity}.GetAllAndCount(Expression, Expression, params Expression[])"/>
        /// </summary>
        /// <typeparam name="TResult"><see cref="IRepository{TEntity}.GetAllAndCount(Expression, Expression, []Expression)"/></typeparam>
        /// <param name="selectBuilder"><see cref="IRepository{TEntity}.GetAllAndCount(Expression, Expression, Expression[])"/></param>
        /// <param name="predicate"><see cref="IRepository{TEntity}.GetAllAndCount(Expression, Expression, Expression[])"/></param>
        /// <param name="includes"><see cref="IRepository{TEntity}.GetAllAndCount(Expression, Expression, Expression[])"/></param>
        /// <returns><see cref="IRepository{TEntity}.GetAllAndCount(Expression, Expression, Expression[])"/></returns>
        public virtual Tuple<IEnumerable<TResult>, int> GetAllAndCount<TResult>(
            Expression<Func<TEntity, TResult>> selectBuilder,
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, IBaseEntity>>[] includes)
                where TResult : IBaseEntity
        {
            IEnumerable<TResult> filtered = _dbSet.Where(selectBuilder, predicate, includes);
            int count = filtered.Count();

            Tuple<IEnumerable<TResult>, int> result = new Tuple<IEnumerable<TResult>, int>(filtered, count);

            return result;
        }

        #endregion
    }
}

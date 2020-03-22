using System;

namespace shaker.data.core
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        void Commit();

        /// <summary>
        /// 
        /// </summary>
        virtual void CommitAndRefreshChanges() { }

        /// <summary>
        /// 
        /// </summary>
        void RollbackChanges();

        /// <summary>
        /// 
        /// </summary>
        IDbSet<TEntity> CreateSet<TEntity>()
            where TEntity : IBaseEntity;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        virtual void SetModified<TEntity>(TEntity entity)
            where TEntity : IBaseEntity
        { }
    }
}

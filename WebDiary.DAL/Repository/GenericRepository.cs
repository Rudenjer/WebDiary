using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using WebDiary.DAL.Pipeline;
using WebDiary.DAL.Repository.Interfaces;

namespace WebDiary.DAL.Repository
{
    public class GenericRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;

            _dbSet = _dbContext.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query.ToList();
        }

        public IEnumerable<TEntity> GetOfSort(IPipeline<TEntity> pipeline)
        {
            return pipeline.Process(_dbSet);
        }

        public virtual TEntity Get(TKey id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public virtual IEnumerable<TResult> Select<TResult>(Func<TEntity, TResult> selector)
        {
            return _dbContext.Set<TEntity>().Select(selector).ToList();
        }

        public virtual void Create(TEntity item)
        {
            _dbContext.Set<TEntity>().Add(item);
        }

        public virtual void Update(TEntity item)
        {
            _dbContext.Entry(item).State = EntityState.Modified;
        }

        public virtual void Delete(TEntity item)
        {
            _dbContext.Set<TEntity>().Remove(item);
        }
    }
}

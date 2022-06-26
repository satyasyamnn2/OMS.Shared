using Microsoft.EntityFrameworkCore;
using OMS.DataAccess.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace OMS.DataAccess.Shared
{
    public class GenericRepository<TDbContext, TEntity> : IRepository<TEntity> where TDbContext : DbContext 
                                                                               where TEntity : class
    {
        private readonly TDbContext _dbContext;
        public GenericRepository(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> InsertOneAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken).ConfigureAwait(false);
            int count = await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return count;
        }

        public async Task<int> InsertManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
            int count = await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return count;
        }
        public async Task<int> UpdateOneAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<TEntity>().Update(entity);
            int count = await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return count;
        }

        public async Task<int> UpdateManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<TEntity>().UpdateRange(entities);
            int count = await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return count;
        }

        public async Task<int> DeleteOneAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            int count = await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return count;
        }

        public async Task<int> DeleteManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
            int count = await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return count;
        }

        public IEnumerable<TDest> Get<TDest>(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", Expression<Func<TEntity, TDest>> select = null)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                if (select == null)
                    return (IEnumerable<TDest>)orderBy(query).ToList();

                return orderBy(query).Select(select).ToList();
            }
            else
            {
                if (select == null) 
                    return query.Select(select).ToList();
                return (IEnumerable<TDest>)query.ToList(); 
            }
        }
    }
}

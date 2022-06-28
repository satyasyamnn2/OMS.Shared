using Microsoft.EntityFrameworkCore;
using OMS.DataAccess.Shared.Contracts;
using OMS.DataAccess.Shared.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace OMS.DataAccess.Shared
{
    public class GenericRepository<TDbContext, TEntity> : IGenericRepository<TEntity> where TDbContext : DbContext
                                                                                      where TEntity : EntityBase
    {
        private readonly TDbContext _dbContext;
        public GenericRepository(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> UpsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            TEntity existingEntity =  await GetByIdAsync(entity.Id, cancellationToken);
            if (existingEntity == null)
                return await InsertOneAsync(entity, cancellationToken);
            return await UpdateOneAsync(entity, cancellationToken);
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
        public async Task<TEntity> GetByIdAsync(string id, CancellationToken cancellationToken = default) 
        {   
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task<IReadOnlyList<TEntity>> GetAll(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<TEntity>().Select(e => e).ToListAsync(cancellationToken);
        }
        public async Task<IReadOnlyList<TDest>> Search<TDest>(Specification<TEntity> specification, Expression<Func<TEntity, TDest>> select, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<TEntity>()
                                   .Where(specification.ToExpression())
                                   .Select(select)
                                   .ToListAsync(cancellationToken);
        }  
    }
}

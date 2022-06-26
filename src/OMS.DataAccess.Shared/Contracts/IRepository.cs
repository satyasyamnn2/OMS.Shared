using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace OMS.DataAccess.Shared.Contracts
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<int> InsertOneAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<int> InsertManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task<int> UpdateOneAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<int> UpdateManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task<int> DeleteOneAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<int> DeleteManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        IEnumerable<TDest> Get<TDest>(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", Expression<Func<TEntity, TDest>> select = null);
    }
}

using OMS.DataAccess.Shared.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace OMS.DataAccess.Shared.Contracts
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<int> UpsertAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<int> InsertOneAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<int> InsertManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task<int> UpdateOneAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<int> UpdateManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task<int> DeleteOneAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<int> DeleteManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task<TEntity> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TEntity>> GetAll(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TDest>> Search<TDest>(Specification<TEntity> specification, Expression<Func<TEntity, TDest>> select = null, CancellationToken cancellationToken = default);
    }
}

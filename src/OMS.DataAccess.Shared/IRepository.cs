using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OMS.DataAccess.Shared
{
    public interface IRepository<TEntity> where TEntity : class 
    {
        Task<int> InsertOneAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<int> InsertManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task<int> UpdateOneAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<int> UpdateManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task<int> DeleteOneAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<int> DeleteManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    }
}

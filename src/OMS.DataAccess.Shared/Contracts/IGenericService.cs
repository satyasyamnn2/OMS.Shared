using OMS.DataAccess.Shared.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace OMS.DataAccess.Shared.Contracts
{
    public interface IGenericService<TEntity, TRepository> where TEntity : class
                                                           where TRepository : IGenericRepository<TEntity>
    {
        TRepository Repository { get; }
        Task<int> SaveEntityAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<TEntity> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TDest>> Search<TDest>(Specification<TEntity> specification, Expression<Func<TEntity, TDest>> select, CancellationToken cancellationToken = default);
    }
}

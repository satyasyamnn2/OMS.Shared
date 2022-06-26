using System.Threading;
using System.Threading.Tasks;

namespace OMS.DataAccess.Shared.Contracts
{
    public interface IGenericService<TEntity, TRepository> where TEntity : class
                                                           where TRepository : IGenericRepository<TEntity>
    {
        TRepository Repository { get; }

        Task<int> SaveEntityAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}

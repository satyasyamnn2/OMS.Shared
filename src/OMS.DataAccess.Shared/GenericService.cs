using OMS.DataAccess.Shared.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace OMS.DataAccess.Shared
{
    public class GenericService<TEntity, TRepository> : IGenericService<TEntity, TRepository> where TEntity : class
                                                                                       where TRepository: IRepository<TEntity>                                                                                             
    {
        private TRepository _repository;
        public GenericService(TRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> SaveEntityAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
           return await _repository.InsertOneAsync(entity);
        }
    }
}

using OMS.DataAccess.Shared.Contracts;
using OMS.DataAccess.Shared.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace OMS.DataAccess.Shared
{
    public class GenericService<TEntity, TRepository> : IGenericService<TEntity, TRepository> where TEntity : class
                                                                                              where TRepository: IGenericRepository<TEntity>                                                                                             
    {
        private TRepository _repository;
        public GenericService(TRepository repository)
        {
            _repository = repository;
        }
        public TRepository Repository { get { return _repository; } }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _repository.GetAll(cancellationToken);
        }

        public async Task<IReadOnlyList<TDest>> Search<TDest>(Specification<TEntity> specification, Expression<Func<TEntity, TDest>> select, CancellationToken cancellationToken = default)
        {
            return await _repository.Search(specification, select, cancellationToken);
        }

        public async Task<TEntity> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _repository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<int> SaveEntityAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
           return await _repository.UpdateOneAsync(entity, cancellationToken);
        }
    }
}

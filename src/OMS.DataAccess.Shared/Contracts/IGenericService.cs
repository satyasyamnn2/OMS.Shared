﻿using System.Threading;
using System.Threading.Tasks;

namespace OMS.DataAccess.Shared.Contracts
{
    public interface IGenericService<TEntity, TRepository> where TEntity : class
                                                           where TRepository : IRepository<TEntity>
    {
        Task<int> SaveEntityAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}
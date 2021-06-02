using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence.EF.Repositories
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);

        Task<T> GetByGuidAsync(Guid guid);

        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}
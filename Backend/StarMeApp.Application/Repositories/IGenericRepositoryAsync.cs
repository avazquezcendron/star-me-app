using StarMeApp.Domain.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarMeApp.Application.Repositories
{
    public interface IGenericRepositoryAsync<T, TId> where T : IBusinessEntity<TId>
    {
        Task<T> GetByIdAsync(TId id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetPagedReponseAsync(int pageNumber, int pageSize);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        void SeEntityUnchanged(T entity);
    }
}

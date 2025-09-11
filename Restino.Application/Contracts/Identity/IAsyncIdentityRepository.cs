using Restino.Domain.Common;

namespace Restino.Application.Contracts.Identity
{
    public interface IAsyncIdentityRepository<T> where T : AuditableEntity
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<T?> GetByUserIdAsync(string userId);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
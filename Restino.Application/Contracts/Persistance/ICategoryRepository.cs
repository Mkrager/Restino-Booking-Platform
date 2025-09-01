using Restino.Domain.Entities;

namespace Restino.Application.Contracts.Persistance
{
    public interface ICategoryRepository : IAsyncRepository<Category>
    {
        Task<List<Category>> GetCategoryWithAccommodationAsync(bool onlyOneCategoryResult, Guid? CategoryId);
        Task<bool> IsCategoryNameUniqueAsync(string name);
    }
}

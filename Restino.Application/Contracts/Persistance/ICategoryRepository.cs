using Restino.Domain.Entities;

namespace Restino.Application.Contracts.Persistance
{
    public interface ICategoryRepository : IAsyncRepository<Category>
    {
        Task<List<Category>> GetCategoryWithAccommodation(bool onlyOneCategoryResult, Guid? CategoryId);
        Task<bool> IsCategoryNameUnique(string name);
    }
}

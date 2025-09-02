using Restino.Domain.Entities;

namespace Restino.Application.Contracts.Persistance
{
    public interface IAccommodationRepository : IAsyncRepository<Accommodation>
    {
        Task<List<Accommodation>> GetAccommodationsWithCategoriesAsync(bool isAccommodationHot);
        Task<List<Accommodation>> GetAccommodationsWithCategoriesByUserIdAsync(string userId);
        Task<List<Accommodation>> SearchAccommodationAsync(string searchString);
        Task<Accommodation?> GetAccommodationWithCategoryByIdAsync(Guid id);
        Task<bool> IsAccommodationNameAndCategoryUniqueAsync(string name, Guid categoryId);
        Task<bool> IsAccommodationNameAndCategoryUniqueForUpdateAsync(string name, Guid categoryId, Guid? accommodationsId);
    }
}

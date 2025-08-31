using Restino.Domain.Entities;

namespace Restino.Application.Contracts.Persistance
{
    public interface IAccommodationRepository : IAsyncRepository<Accommodation>
    {
        Task<List<Accommodation>> GetAccommodationsWithCategoriesAsync(bool isAccommodationHot);
        Task<List<Accommodation>> GetAccommodationsWithCategoriesByUserIdAsync(string userId);
        Task<List<Accommodation>> SearchAccommodation(string searchString);
        Task<bool> IsAccommodationNameAndCategoryUnique(string name, Guid categoryId);
        Task<bool> IsAccommodationNameAndCategoryUniqueUpdate(string name, Guid categoryId, Guid? accommodationsId);
    }
}

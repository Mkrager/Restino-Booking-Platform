using Restino.Domain.Entities;

namespace Restino.Application.Contracts.Persistance
{
    public interface IAccommodationRepository : IAsyncRepository<Accommodation>
    {
        Task<List<Accommodation>> GetAccommodationsWithCategoriesAsync(bool isAccommodationHot);
        Task<bool> IsAccommodationNameAndCategoryUnique(string name, Guid categoryId);
        Task<bool> IsAccommodationNameAndCategoryUniqueUpdate(string name, Guid categoryId, Guid? accommodationsId);
        Task<List<Accommodation>> SearchAccommodation(string searchString);
        Task<List<Accommodation>> ListUserAccommodations(string userId);
    }
}

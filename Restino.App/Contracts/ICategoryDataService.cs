using Restino.App.Infrastructure.Api;
using Restino.App.ViewModels;

namespace Restino.App.Contracts
{
    public interface ICategoryDataService
    {
        Task<List<CategoryViewModel>> GetAllCategories();
        Task<List<CategoryViewModel>> GetAllCategoriesWithAccommodations(bool onlyOneCategoryResult, Guid? categoryId);
        Task<ApiResponse<Guid>> CreateCategory(CategoryViewModel categoryViewModel);
        Task<CategoryDetailsViewModel> GetCategoryById(Guid id);
        Task<ApiResponse> DeleteCategory(Guid id);
    }
}

using Restino.App.Contracts;
using Restino.App.Infrastructure.Api;
using Restino.App.Infrastructure.BaseServices;
using Restino.App.ViewModels;
using System.Text;
using System.Text.Json;

namespace Restino.App.Services
{
    public class CategoryDataService : BaseDataService, ICategoryDataService
    {
        public CategoryDataService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<ApiResponse<Guid>> CreateCategory(CategoryViewModel categoryViewModel)
        {
            try
            {
                var content = new StringContent(
                    JsonSerializer.Serialize(categoryViewModel),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync("category", content);

                if (response.IsSuccessStatusCode)
                {
                    var categoryId = await DeserializeResponse<Guid>(response);
                    return new ApiResponse<Guid>(System.Net.HttpStatusCode.OK, categoryId);
                }

                var errorMessages = await DeserializeResponse<List<string>>(response); 
                return new ApiResponse<Guid>(System.Net.HttpStatusCode.BadRequest, Guid.Empty, errorMessages.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new ApiResponse<Guid>(System.Net.HttpStatusCode.BadRequest, Guid.Empty, ex.Message);
            }
        }

        public async Task<ApiResponse> DeleteCategory(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"category/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return new ApiResponse(System.Net.HttpStatusCode.OK);
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessages = JsonSerializer.Deserialize<List<string>>(errorContent);
                return new ApiResponse(System.Net.HttpStatusCode.BadRequest, errorMessages.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new ApiResponse(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
        }

        public async Task<List<CategoryViewModel>> GetAllCategories()
        {
            var response = await _httpClient.GetAsync($"category");

            if (response.IsSuccessStatusCode)
            {
                var categoriesList = await DeserializeResponse<List<CategoryViewModel>>(response);
                return categoriesList;
            }
            return new List<CategoryViewModel>();
        }

        public async Task<List<CategoryViewModel>> GetAllCategoriesWithAccommodations(bool onlyOneCategoryResult, Guid? categoryId)
        {
            var queryString = $"?onlyOneCategoryResult={onlyOneCategoryResult.ToString().ToLower()}"
                            + (categoryId.HasValue ? $"&categoryId={categoryId.Value}" : "");

            var response = await _httpClient.GetAsync($"category/accommodations{queryString}");

            if (response.IsSuccessStatusCode)
            {
                var categoriesWithAccommodations = await DeserializeResponse<List<CategoryViewModel>>(response);
                return categoriesWithAccommodations;
            }

            return new List<CategoryViewModel>();
        }

        public async Task<CategoryDetailsViewModel> GetCategoryById(Guid id)
        {
            var response = await _httpClient.GetAsync($"category/{id}");

            if (response.IsSuccessStatusCode)
            {
                var categoryDetails = await DeserializeResponse<CategoryDetailsViewModel>(response);
                return categoryDetails;
            }

            return new CategoryDetailsViewModel();
        }
    }
}
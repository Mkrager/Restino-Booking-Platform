using Restino.App.Infrastructure.Api;
using Restino.App.ViewModels;

namespace Restino.App.Contracts
{
    public interface IAccommodationDataService
    {
        Task<List<AccommodationListViewModel>> GetAllAccommodations(bool isHotAccommdoation = false);
        Task<List<AccommodationListViewModel>> GetAllUserAccommodations();
        Task<ApiResponse<List<AccommodationListViewModel>>> SearchAccommodation(string searchString);
        Task<AccommodationDetailViewModel> GetAccommodationById(Guid id);
        Task<ApiResponse<Guid>> CreateAccommodation(AccommodationDetailViewModel accommodationDetailViewModel);
        Task<ApiResponse> UpdateAccommodation(AccommodationDetailViewModel accommodationDetailViewModel);
        Task<ApiResponse> DeleteAccommodation(Guid id);
    }
}

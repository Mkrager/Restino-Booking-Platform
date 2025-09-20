using Restino.App.Infrastructure.Api;
using Restino.App.ViewModels;

namespace Restino.App.Contracts
{
    public interface IReservationDataService
    {
        Task<ApiResponse<Guid>> CreateReservation(ReservationDetailViewModel reservationDetailViewModel);
        Task<List<ReservationListViewModel>> GetAllReservation();
        Task<List<ReservationListViewModel>> GetUserReservation(string userId);
        Task<ReservationDetailViewModel> GetReservationById(Guid id);
        Task<ApiResponse> DeleteReservation(Guid id);
    }
}

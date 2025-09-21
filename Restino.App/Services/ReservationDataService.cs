using Restino.App.Contracts;
using Restino.App.Infrastructure.Api;
using Restino.App.Infrastructure.BaseServices;
using Restino.App.ViewModels.Reservation;
using System.Text;
using System.Text.Json;

namespace Restino.App.Services
{
    public class ReservationDataService : BaseDataService, IReservationDataService
    {
        public ReservationDataService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<ApiResponse<Guid>> CreateReservation(ReservationDetailViewModel reservationDetailViewModel)
        {
            try
            {
                var content = new StringContent(
                    JsonSerializer.Serialize(reservationDetailViewModel),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync("reservation", content);

                if (response.IsSuccessStatusCode)
                {
                    var reservation = await DeserializeResponse<Guid>(response);
                    return new ApiResponse<Guid>(System.Net.HttpStatusCode.OK, reservation);
                }

                var errorMessages = await DeserializeResponse<List<string>>(response);
                return new ApiResponse<Guid>(System.Net.HttpStatusCode.BadRequest, Guid.Empty, errorMessages.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new ApiResponse<Guid>(System.Net.HttpStatusCode.BadRequest, Guid.Empty, ex.Message);
            }
        }

        public async Task<List<ReservationListViewModel>> GetAllReservation()
        {
            var response = await _httpClient.GetAsync("reservation");

            if (response.IsSuccessStatusCode)
            {
                var reservations = await DeserializeResponse<List<ReservationListViewModel>>(response);
                return reservations;    
            }

            return new List<ReservationListViewModel>();
        }

        public async Task<ReservationDetailViewModel> GetReservationById(Guid id)
        {
            var response = await _httpClient.GetAsync($"reservation/{id}");

            if (response.IsSuccessStatusCode)
            {
                var reservationDetails = await DeserializeResponse<ReservationDetailViewModel>(response);
                return reservationDetails;
            }

            return new ReservationDetailViewModel();
        }

        public async Task<ApiResponse> DeleteReservation(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"reservation/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return new ApiResponse(System.Net.HttpStatusCode.NoContent);
                }

                var errorMessages = await DeserializeResponse<List<string>>(response);
                return new ApiResponse(System.Net.HttpStatusCode.BadRequest, errorMessages.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new ApiResponse(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
        }

        public async Task<List<ReservationListViewModel>> GetUserReservation()
        {
            var response = await _httpClient.GetAsync("reservation/user");

            if (response.IsSuccessStatusCode)
            {
                var reservations = await DeserializeResponse<List<ReservationListViewModel>>(response);
                return reservations;    
            }

            return new List<ReservationListViewModel>();
        }
    }
}
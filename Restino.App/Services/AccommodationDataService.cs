using Restino.App.Contracts;
using Restino.App.Infrastructure.Api;
using Restino.App.Infrastructure.BaseServices;
using Restino.App.ViewModels;
using System.Text;
using System.Text.Json;

namespace Restino.App.Services
{
    public class AccommodationDataService : BaseDataService, IAccommodationDataService
    {
        public AccommodationDataService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<ApiResponse<Guid>> CreateAccommodation(AccommodationDetailViewModel accommodationDetailViewModel)
        {
            try
            {
                var content = new StringContent(
                    JsonSerializer.Serialize(accommodationDetailViewModel),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync("accommodation", content);

                if (response.IsSuccessStatusCode)
                {
                    var accommodation = await DeserializeResponse<Guid>(response);

                    return new ApiResponse<Guid>(System.Net.HttpStatusCode.OK, accommodation);
                }

                var errorMessages = await DeserializeResponse<List<string>>(response);

                return new ApiResponse<Guid>(System.Net.HttpStatusCode.BadRequest, Guid.Empty, errorMessages.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new ApiResponse<Guid>(System.Net.HttpStatusCode.BadRequest, Guid.Empty, ex.Message);
            }
        }

        public async Task<ApiResponse> DeleteAccommodation(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"accommodation/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return new ApiResponse(System.Net.HttpStatusCode.OK);
                }

                var errorMessages = await DeserializeResponse<List<string>>(response);
                return new ApiResponse(System.Net.HttpStatusCode.BadRequest, errorMessages.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new ApiResponse(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
        }

        public async Task<AccommodationDetailViewModel> GetAccommodationById(Guid id)
        {
            var response = await _httpClient.GetAsync($"accommodation/{id}");

            if (response.IsSuccessStatusCode)
            {
                var accommodationDetails = await DeserializeResponse<AccommodationDetailViewModel>(response);
                return accommodationDetails;
            }

            return new AccommodationDetailViewModel();
        }

        public async Task<List<AccommodationListViewModel>> GetAllAccommodations(bool isAccommodationHot = false)
        {
            var response = await _httpClient.GetAsync($"accommodation?isHot={isAccommodationHot}");

            if (response.IsSuccessStatusCode)
            {
                var accommodations = await DeserializeResponse<List<AccommodationListViewModel>>(response);
                return accommodations;
            }

            return new List<AccommodationListViewModel>();
        }

        public async Task<List<AccommodationListViewModel>> GetAllUserAccommodations()
        {
            var response = await _httpClient.GetAsync("accommodation/user");

            if (response.IsSuccessStatusCode)
            {
                var userAccommodation = await DeserializeResponse<List<AccommodationListViewModel>>(response);
                return userAccommodation;
            }

            return new List<AccommodationListViewModel>();
        }

        public async Task<ApiResponse<List<AccommodationListViewModel>>> SearchAccommodation(string searchString)
        {
            try
            {
                var response = await _httpClient.GetAsync($"accommodation/search?searchString={searchString}");

                if (response.IsSuccessStatusCode)
                {
                    var accommodations = await DeserializeResponse<List<AccommodationListViewModel>>(response);
                    return new ApiResponse<List<AccommodationListViewModel>>(System.Net.HttpStatusCode.OK, accommodations);
                }

                var errorMessages = await DeserializeResponse<List<string>>(response);
                return new ApiResponse<List<AccommodationListViewModel>>(System.Net.HttpStatusCode.BadRequest, new List<AccommodationListViewModel>(), errorMessages.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<AccommodationListViewModel>>(System.Net.HttpStatusCode.BadRequest, new List<AccommodationListViewModel>(), ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateAccommodation(AccommodationDetailViewModel accommodationDetailViewModel)
        {
            try
            {
                var content = new StringContent(
                    JsonSerializer.Serialize(accommodationDetailViewModel),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PutAsync($"accommodation", content);

                if (response.IsSuccessStatusCode)
                {
                    return new ApiResponse(System.Net.HttpStatusCode.OK);
                }

                var errorMessages = await DeserializeResponse<List<string>>(response);
                return new ApiResponse(System.Net.HttpStatusCode.BadRequest, errorMessages.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new ApiResponse(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
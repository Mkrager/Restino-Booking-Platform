using Restino.App.Contracts;
using Restino.App.Infrastructure.Api;
using Restino.App.Infrastructure.BaseServices;
using Restino.App.ViewModels.User;

namespace Restino.App.Services
{
    public class UserDataService : BaseDataService, IUserDataService
    {
        public UserDataService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<GetUserDetailsResponse> GetUserById(string userId)
        {
            var response = await _httpClient.GetAsync($"user/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var userDetails = await DeserializeResponse<GetUserDetailsResponse>(response);
                return userDetails;
            }

            return new GetUserDetailsResponse();
        }

        public async Task<GetUserDetailsResponse> GetUserDetails()
        {
            var response = await _httpClient.GetAsync("user/details");

            if (response.IsSuccessStatusCode)
            {
                var userDetails = await DeserializeResponse<GetUserDetailsResponse>(response);
                return userDetails;
            }

            return new GetUserDetailsResponse();
        }

        public async Task<List<GetUserDetailsResponse>> GetUserList()
        {
            var response = await _httpClient.GetAsync("user/all");

            if (response.IsSuccessStatusCode)
            {
                var users = await DeserializeResponse<List<GetUserDetailsResponse>>(response);
                return users;
            }
            return new List<GetUserDetailsResponse>();
        }

        public async Task<List<SearchUserResponse>> SearchUser(string searchInput)
        {
            var response = await _httpClient.GetAsync($"user/search?userName={searchInput}");

            if (response.IsSuccessStatusCode)
            {
                var users = await DeserializeResponse<List<SearchUserResponse>>(response);
                return users;
            }
            return new List<SearchUserResponse>();
        }

        public async Task<ApiResponse> DeleteUser(string id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"user/{id}");

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
    }
}
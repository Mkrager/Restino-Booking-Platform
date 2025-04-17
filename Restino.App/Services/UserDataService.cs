using Restino.App.Contracts;
using Restino.App.ViewModels;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Restino.App.Services
{
    public class UserDataService : IUserDataService
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly HttpClient _httpClient;

        public UserDataService(IAuthenticationService authenticationService, HttpClient httpClient)
        {
            _authenticationService = authenticationService;
            _httpClient = httpClient;
        }
        public async Task<GetUserDetailsResponse> GetUserDetails(string userId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7288/api/User/GetById/{userId}");

            string accessToken = _authenticationService.GetAccessToken();

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var userDetails = JsonSerializer.Deserialize<GetUserDetailsResponse>(responseContent);

                return userDetails;
            }

            return new GetUserDetailsResponse();
        }

        public async Task<List<GetUserDetailsResponse>> GetUserList()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7288/api/User");

            string accessToken = _authenticationService.GetAccessToken();

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var users = JsonSerializer.Deserialize<List<GetUserDetailsResponse>>(responseContent);

                return users;
            }
            return new List<GetUserDetailsResponse>();
        }

        public async Task<List<SearchUserResponse>> SearchUser(string searchInput)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7288/api/User/SearchUser/{searchInput}");

            string accessToken = _authenticationService.GetAccessToken();

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var users = JsonSerializer.Deserialize<List<SearchUserResponse>>(responseContent);

                return users;
            }
            return new List<SearchUserResponse>();
        }

        public async Task<ApiResponse<bool>> DeleteUser(string id)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7288/api/User/Delete/{id}");

                string accessToken = _authenticationService.GetAccessToken();

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    return new ApiResponse<bool>(System.Net.HttpStatusCode.NoContent, true);
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessages = JsonSerializer.Deserialize<List<string>>(errorContent);
                return new ApiResponse<bool>(System.Net.HttpStatusCode.BadRequest, false, errorMessages.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(System.Net.HttpStatusCode.BadRequest, false, ex.Message);
            }
        }

        public async Task<ApiResponse<bool>> SendPasswordResetCodeAsync(string email)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:7288/api/User/SendPasswordResetCode/{email}");

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    return new ApiResponse<bool>(System.Net.HttpStatusCode.OK, true);
                }

                var errorContent = await response.Content.ReadAsStringAsync();

                var errorMessages = JsonSerializer.Deserialize<Dictionary<string, string>>(errorContent) ??
                                    new Dictionary<string, string> { { "error", errorContent } };

                return new ApiResponse<bool>(System.Net.HttpStatusCode.BadRequest, false, errorMessages.GetValueOrDefault("error"));
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(System.Net.HttpStatusCode.BadRequest, false, ex.Message);
            }
        }

        public async Task<ApiResponse<bool>> ChangePasswordAsync(string email, string newPassword, string code)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:7288/api/User/ChangePassword/{newPassword}/{email}/{code}");

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    return new ApiResponse<bool>(System.Net.HttpStatusCode.OK, true);
                }

                var errorContent = await response.Content.ReadAsStringAsync();

                var errorMessages = JsonSerializer.Deserialize<Dictionary<string, string>>(errorContent) ??
                                    new Dictionary<string, string> { { "error", errorContent } };

                return new ApiResponse<bool>(System.Net.HttpStatusCode.BadRequest, false, errorMessages.GetValueOrDefault("error"));
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(System.Net.HttpStatusCode.BadRequest, false, ex.Message);
            }
        }

        public async Task<ApiResponse<bool>> AddTwoFactorAsync(string email, string twoFactorCode)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:7288/api/User/AddTwoFactorAuthCode/{email}/{twoFactorCode}");

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    return new ApiResponse<bool>(System.Net.HttpStatusCode.OK, true);
                }

                var errorContent = await response.Content.ReadAsStringAsync();

                var errorMessages = JsonSerializer.Deserialize<Dictionary<string, string>>(errorContent) ??
                                    new Dictionary<string, string> { { "error", errorContent } };

                return new ApiResponse<bool>(System.Net.HttpStatusCode.BadRequest, false, errorMessages.GetValueOrDefault("error"));
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(System.Net.HttpStatusCode.BadRequest, false, ex.Message);
            }
        }

        public async Task<ApiResponse<bool>> DeleteTwoFactorAsync(string email, string twoFactorCode)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7288/api/User/DeleteTwoFactorAuthCode/{email}/{twoFactorCode}");

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    return new ApiResponse<bool>(System.Net.HttpStatusCode.OK, true);
                }

                var errorContent = await response.Content.ReadAsStringAsync();

                var errorMessages = JsonSerializer.Deserialize<Dictionary<string, string>>(errorContent) ??
                                    new Dictionary<string, string> { { "error", errorContent } };

                return new ApiResponse<bool>(System.Net.HttpStatusCode.BadRequest, false, errorMessages.GetValueOrDefault("error"));
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(System.Net.HttpStatusCode.BadRequest, false, ex.Message);
            }
        }

        public async Task<ApiResponse<bool>> SendTwoFactorCodeAsync(string email)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:7288/api/User/SendTwoFactorAuthCode/{email}");

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    return new ApiResponse<bool>(System.Net.HttpStatusCode.OK, true);
                }

                var errorContent = await response.Content.ReadAsStringAsync();

                var errorMessages = JsonSerializer.Deserialize<Dictionary<string, string>>(errorContent) ??
                                    new Dictionary<string, string> { { "error", errorContent } };

                return new ApiResponse<bool>(System.Net.HttpStatusCode.BadRequest, false, errorMessages.GetValueOrDefault("error"));
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(System.Net.HttpStatusCode.BadRequest, false, ex.Message);
            }
        }
    }
}

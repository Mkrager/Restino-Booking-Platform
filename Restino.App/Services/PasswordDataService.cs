using Restino.App.Contracts;
using Restino.App.Infrastructure.Api;
using Restino.App.Infrastructure.BaseServices;
using Restino.App.ViewModels.ResetPassword;
using System.Text;
using System.Text.Json;

namespace Restino.App.Services
{
    public class PasswordDataService : BaseDataService, IPasswordDataService
    {
        public PasswordDataService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<ApiResponse> SendPasswordResetCodeAsync(SendPasswordResetCodeRequest sendPasswordResetCodeRequest)
        {
            try
            {
                var content = new StringContent(
                    JsonSerializer.Serialize(sendPasswordResetCodeRequest),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync("password/reset-code", content);

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

        public async Task<ApiResponse> ResetPasswordAsync(ResetPasswordRequest resetPasswordRequest)
        {
            try
            {
                var content = new StringContent(
                    JsonSerializer.Serialize(resetPasswordRequest),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PutAsync("password/change", content);

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
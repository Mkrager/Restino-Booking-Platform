using Restino.App.Contracts;
using Restino.App.Infrastructure.Api;
using Restino.App.Infrastructure.BaseServices;
using Restino.App.ViewModels.TwoFactor;
using System.Text;
using System.Text.Json;

namespace Restino.App.Services
{
    public class TwoFactorDataService : BaseDataService, ITwoFactorDataService
    {
        public TwoFactorDataService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<ApiResponse> AddTwoFactorAsync(AddTwoFactorRequest addTwoFactorRequest)
        {
            try
            {
                var content = new StringContent(
                    JsonSerializer.Serialize(addTwoFactorRequest),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync("twofactor/add", content);

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

        public async Task<ApiResponse> DeleteTwoFactorAsync(DeleteTwoFactorRequest deleteTwoFactorRequest)
        {
            try
            {
                var content = new StringContent(
                    JsonSerializer.Serialize(deleteTwoFactorRequest),
                    Encoding.UTF8,
                    "application/json");

                var request = new HttpRequestMessage(HttpMethod.Delete, "twofactor/remove")
                {
                    Content = content
                };

                var response = await _httpClient.SendAsync(request);

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

        public async Task<ApiResponse> SendTwoFactorCodeAsync(SendTwoFactorCodeRequest sendTwoFactorCodeRequest)
        {
            try
            {
                var content = new StringContent(
                    JsonSerializer.Serialize(sendTwoFactorCodeRequest),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync("twofactor/send", content);

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
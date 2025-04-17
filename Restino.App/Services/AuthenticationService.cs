using Restino.App.Contracts;
using Restino.App.ViewModels;
using System.Text;
using System.Text.Json;

namespace Restino.App.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;

        public AuthenticationService(IHttpContextAccessor httpContextAccessor, HttpClient httpClient)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<LoginRequest>> Authenticate(string email, string password)
        {
            try
            {
                var authenticationRequest = new LoginResponse() { Email = email, Password = password };

                var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7288/api/Account/authenticate")
                {
                    Content = new StringContent(JsonSerializer.Serialize(authenticationRequest), Encoding.UTF8, "application/json")
                };

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var authenticationResponse = JsonSerializer.Deserialize<LoginRequest>(responseContent);

                    var loginResponse = new LoginRequest()
                    {
                        Email = authenticationResponse.Email,
                        Id = authenticationResponse.Id,
                        Token = authenticationResponse.Token,
                        TwoFactorRequired = authenticationResponse.TwoFactorRequired,
                        UserName = authenticationResponse.UserName
                    };

                    if (authenticationResponse.TwoFactorRequired)
                    {
                        return new ApiResponse<LoginRequest>(System.Net.HttpStatusCode.OK, loginResponse);
                    }

                    var jwtToken = authenticationResponse?.Token;

                    if (!string.IsNullOrEmpty(jwtToken))
                    {
                        _httpContextAccessor.HttpContext.Response.Cookies.Append("access_token", jwtToken, new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            SameSite = SameSiteMode.Strict,
                            Expires = DateTime.UtcNow.AddDays(30)
                        });

                        return new ApiResponse<LoginRequest>(System.Net.HttpStatusCode.OK, loginResponse);
                    }
                }

                var errorContent = await response.Content.ReadAsStringAsync();

                var errorMessages = JsonSerializer.Deserialize<Dictionary<string, string>>(errorContent) ??
                                    new Dictionary<string, string> { { "error", errorContent } };

                return new ApiResponse<LoginRequest>(System.Net.HttpStatusCode.BadRequest, null, errorMessages.GetValueOrDefault("error"));
            }
            catch (Exception ex)
            {
                return new ApiResponse<LoginRequest>(System.Net.HttpStatusCode.BadRequest, null, ex.Message);
            }
        }

        public Task Logout()
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("access_token");
            return Task.CompletedTask;
        }

        public async Task<ApiResponse<bool>> Register(string firstName, string lastName, string userName, string email, string password)
        {
            try
            {
                var registrationRequest = new RegistrationRequest() { Password = password, Email = email, FirstName = firstName, LastName = lastName, UserName = userName };

                var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7288/api/account/register")
                {
                    Content = new StringContent(JsonSerializer.Serialize(registrationRequest), Encoding.UTF8, "application/json")
                };

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
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

        public string GetAccessToken()
        {
            return _httpContextAccessor.HttpContext.Request.Cookies["access_token"];
        }

        public async Task<ApiResponse<LoginRequest>> AuthenticateTwoFactor(string email, string twoFactorCode)
        {
            try
            {
                var authenticationRequest = new VerifyTwoFactorCodeResponse() { Email = email, TwoFactorCode = twoFactorCode };

                var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7288/api/Account/authenticateTwoFactor")
                {
                    Content = new StringContent(JsonSerializer.Serialize(authenticationRequest), Encoding.UTF8, "application/json")
                };

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var authenticationResponse = JsonSerializer.Deserialize<LoginRequest>(responseContent);

                    var loginResponse = new LoginRequest()
                    {
                        Email = authenticationResponse.Email,
                        Id = authenticationResponse.Id,
                        Token = authenticationResponse.Token,
                        TwoFactorRequired = authenticationResponse.TwoFactorRequired,
                        UserName = authenticationResponse.UserName
                    };

                    if (authenticationResponse.TwoFactorRequired)
                    {
                        return new ApiResponse<LoginRequest>(System.Net.HttpStatusCode.OK, loginResponse);
                    }

                    var jwtToken = authenticationResponse?.Token;

                    if (!string.IsNullOrEmpty(jwtToken))
                    {
                        _httpContextAccessor.HttpContext.Response.Cookies.Append("access_token", jwtToken, new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            SameSite = SameSiteMode.Strict,
                            Expires = DateTime.UtcNow.AddDays(30)
                        });

                        return new ApiResponse<LoginRequest>(System.Net.HttpStatusCode.OK, loginResponse);
                    }
                }

                var errorContent = await response.Content.ReadAsStringAsync();

                var errorMessages = JsonSerializer.Deserialize<Dictionary<string, string>>(errorContent) ??
                                    new Dictionary<string, string> { { "error", errorContent } };

                return new ApiResponse<LoginRequest>(System.Net.HttpStatusCode.BadRequest, null, errorMessages.GetValueOrDefault("error"));
            }
            catch (Exception ex)
            {
                return new ApiResponse<LoginRequest>(System.Net.HttpStatusCode.BadRequest, null, ex.Message);
            }
        }
    }
}

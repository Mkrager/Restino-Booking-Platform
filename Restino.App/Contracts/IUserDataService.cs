using Restino.App.Infrastructure.Api;
using Restino.App.ViewModels.ResetPassword;
using Restino.App.ViewModels.TwoFactor;
using Restino.App.ViewModels.User;

namespace Restino.App.Contracts
{
    public interface IUserDataService
    {
        Task<GetUserDetailsResponse> GetUserById(string userId);
        Task<GetUserDetailsResponse> GetUserDetails();
        Task<List<GetUserDetailsResponse>> GetUserList();
        Task<List<SearchUserResponse>> SearchUser(string searchInput);
        Task<ApiResponse> DeleteUser(string id);
        Task<ApiResponse> SendPasswordResetCodeAsync(SendPasswordResetCodeRequest sendPasswordResetCodeRequest);
        Task<ApiResponse> ResetPasswordAsync(ResetPasswordRequest resetPasswordRequest);
        Task<ApiResponse> AddTwoFactorAsync(AddTwoFactorRequest addTwoFactorRequest);
        Task<ApiResponse> DeleteTwoFactorAsync(DeleteTwoFactorRequest deleteTwoFactorRequest);
        Task<ApiResponse> SendTwoFactorCodeAsync(SendTwoFactorCodeRequest sendTwoFactorCodeRequest);
    }
}

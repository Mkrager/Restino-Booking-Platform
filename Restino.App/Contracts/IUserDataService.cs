using Restino.App.Services;
using Restino.App.ViewModels;

namespace Restino.App.Contracts
{
    public interface IUserDataService
    {
        Task<GetUserDetailsResponse> GetUserDetails(string userId);
        Task<List<GetUserDetailsResponse>> GetUserList();
        Task<List<SearchUserResponse>> SearchUser(string searchInput);
        Task<ApiResponse<bool>> DeleteUser(string id);
        Task<ApiResponse<bool>> SendPasswordResetCodeAsync(string email);
        Task<ApiResponse<bool>> ChangePasswordAsync(string email, string newPassword, string code);
    }
}

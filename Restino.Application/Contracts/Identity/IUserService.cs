using Restino.Application.DTOs.Authentication;

namespace Restino.Application.Contracts.Identity
{
    public interface IUserService
    {
        Task<GetUserDetailsResponse> GetUserDetailsAsync(string userId);
        Task<List<GetUserDetailsResponse>> GetUserListAsync();
        Task<List<SearchUserResponse>> SearchUser(string searchInput);
        Task DeleteUser(string userId);
        Task ResetPasswordAsync(string email, string code, string newPassword);
        //Task SendPasswordResetCodeAsync(string email);
        Task AddTwoFactorAsync(string email, string twoFactorCode);
        Task DeleteTwoFactorAsync(string email, string twoFactorCode);
        //Task SendTwoFactorCodeAsync(string email);
    }
}

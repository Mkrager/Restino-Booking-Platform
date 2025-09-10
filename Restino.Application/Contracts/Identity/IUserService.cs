using Restino.Application.DTOs.Authentication;

namespace Restino.Application.Contracts.Identity
{
    public interface IUserService
    {
        Task<GetUserDetailsResponse> GetUserDetailsAsync(string userId);
        Task<List<GetUserDetailsResponse>> GetUserListAsync();
        Task<List<SearchUserResponse>> SearchUser(string searchInput);
        
        Task DeleteUser(string userId);

        //Task SendPasswordResetCodeAsync(string email);

        //Task ResetPasswordAsync(string email, string code, string newPassword);

        Task AddTwoFactorToAccountAsync(string email);
        Task DeleteTwoFactorFromAccountAsync(string email);
    }
}

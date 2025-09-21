using Restino.App.Infrastructure.Api;
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
    }
}
using Restino.App.Infrastructure.Api;
using Restino.App.ViewModels.TwoFactor;

namespace Restino.App.Contracts
{
    public interface ITwoFactorDataService
    {
        Task<ApiResponse> AddTwoFactorAsync(AddTwoFactorRequest addTwoFactorRequest);
        Task<ApiResponse> DeleteTwoFactorAsync(DeleteTwoFactorRequest deleteTwoFactorRequest);
        Task<ApiResponse> SendTwoFactorCodeAsync(SendTwoFactorCodeRequest sendTwoFactorCodeRequest);
    }
}

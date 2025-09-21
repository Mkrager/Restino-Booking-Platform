using Restino.App.Infrastructure.Api;
using Restino.App.ViewModels.ResetPassword;

namespace Restino.App.Contracts
{
    public interface IPasswordDataService
    {
        Task<ApiResponse> SendPasswordResetCodeAsync(SendPasswordResetCodeRequest sendPasswordResetCodeRequest);
        Task<ApiResponse> ResetPasswordAsync(ResetPasswordRequest resetPasswordRequest);
    }
}

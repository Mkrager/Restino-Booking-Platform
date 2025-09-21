namespace Restino.App.ViewModels.ResetPassword
{
    public class ResetPasswordRequest
    {
        public string Email { get; set; } = string.Empty;
        public string ResetPasswordCode { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}

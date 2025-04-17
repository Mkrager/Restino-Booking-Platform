namespace Restino.App.ViewModels
{
    public class VerifyTwoFactorCodeResponse
    {
        public string Email { get; set; } = string.Empty;
        public string TwoFactorCode { get; set; } = string.Empty;
    }
}

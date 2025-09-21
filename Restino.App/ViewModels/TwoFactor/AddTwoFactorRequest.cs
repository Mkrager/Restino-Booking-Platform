namespace Restino.App.ViewModels.TwoFactor
{
    public class AddTwoFactorRequest
    {
        public string Email { get; set; } = string.Empty;
        public string TwoFactorCode { get; set; } = string.Empty;
    }
}

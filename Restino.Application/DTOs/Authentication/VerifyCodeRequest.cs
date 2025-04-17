namespace Restino.Application.DTOs.Authentication
{
    public class VerifyCodeRequest
    {
        public string Email { get; set; } = string.Empty;
        public string TwoFactorCode { get; set; } = string.Empty;
    }
}

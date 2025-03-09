using System.ComponentModel.DataAnnotations;

namespace Restino.App.ViewModels
{
    public class ChangePasswordVm
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Token { get; set; } = string.Empty;
        [Required]
        public string NewPassword { get; set; } = string.Empty;
    }
}

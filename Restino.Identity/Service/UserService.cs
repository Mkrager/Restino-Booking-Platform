using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restino.Application.Contracts.Identity;
using Restino.Application.Contracts.Infrastructure;
using Restino.Application.DTOs.Authentication;
using Restino.Application.DTOs.Mail;
using Restino.Identity.Models;

namespace Restino.Identity.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;

        public UserService(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, IEmailService emailService)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
        }
        public async Task<GetUserDetailsResponse> GetUserDetailsAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new Exception($"User not found.");
            }

            GetUserDetailsResponse response = new GetUserDetailsResponse
            {
                UserId = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName
            };

            return response;
        }

        public Task<string> GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("uid")?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("User is not authenticated.");
            }

            return Task.FromResult(userId);
        }

        public Task<string> GetUserRole()
        {
            var userRole = _httpContextAccessor.HttpContext?.User?.FindFirst("roles")?.Value;

            if (string.IsNullOrEmpty(userRole))
            {
                throw new Exception("User is not authenticated.");
            }

            return Task.FromResult(userRole);
        }

        public async Task<List<SearchUserResponse>> SearchUser(string searchInput)
        {
            if (string.IsNullOrWhiteSpace(searchInput))
            {
                throw new ArgumentException("Search input cannot be empty.");
            }

            var users = await _userManager.Users
                .Where(u => u.UserName.Contains(searchInput) || u.Id.Equals(searchInput))
                .ToListAsync();

            var response = users.Select(user => new SearchUserResponse
            {
                UserId = user.Id,
                UserName = user.UserName,
            }).ToList();

            return response;
        }

        public async Task<List<GetUserDetailsResponse>> GetUserListAsync()
        {
            var users = _userManager.Users.ToList();

            var userDetailsList = users.Select(user => new GetUserDetailsResponse
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email
            }).ToList();

            return userDetailsList;
        }

        public async Task DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var result = await _userManager.DeleteAsync(user);
        }

        public async Task SendPasswordResetCodeAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var random = new Random();
            var randomCode = new string(Enumerable.Range(0, 6).Select(_ => chars[random.Next(chars.Length)]).ToArray());

            user.Code = randomCode;

            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
                throw new Exception("Failed");

            var emailContent = $"Your password reset code: {randomCode}";

            var emailToSend = new Email
            {
                To = user.Email,
                Subject = "Password Reset Code",
                Body = emailContent
            };

            bool emailSent = await _emailService.SendEmail(emailToSend);

            if (!emailSent)
            {
                throw new Exception("Email sending failed.");
            }
        }

        public async Task ResetPasswordAsync(string email, string code, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            if (code != user.Code)
                throw new Exception("Incorrect code");

            var removePasswordResult = await _userManager.RemovePasswordAsync(user);
            if (!removePasswordResult.Succeeded)
            {
                var errors = string.Join(", ", removePasswordResult.Errors.Select(e => e.Description));
                throw new Exception($"Failed to remove old password: {errors}");
            }

            var addPasswordResult = await _userManager.AddPasswordAsync(user, newPassword);
            if (!addPasswordResult.Succeeded)
            {
                var errors = string.Join(", ", addPasswordResult.Errors.Select(e => e.Description));
                throw new Exception($"Password reset failed: {errors}");
            }

            user.Code = null;

            var updateResult = await _userManager.UpdateAsync(user);
        }

        public async Task AddTwoFactorAsync(string email, string twoFactorCode)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            if (twoFactorCode != user.TwoFactorCode)
                throw new Exception("Incorrect code");

            if (user.TwoFactorCodeDuration <= DateTime.Now)
                throw new Exception("Code expired");

            user.TwoFactorCode = null;
            user.TwoFactorCodeDuration = null;
            user.TwoFactorEnabled = true;

            var updateResult = await _userManager.UpdateAsync(user);
        }

        public async Task DeleteTwoFactorAsync(string email, string twoFactorCode)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            if (twoFactorCode != user.TwoFactorCode)
                throw new Exception("Incorrect code");

            if (user.TwoFactorCodeDuration <= DateTime.Now)
                throw new Exception("Code expired");

            user.TwoFactorCode = null;
            user.TwoFactorCodeDuration = null;
            user.TwoFactorEnabled = false;

            var updateResult = await _userManager.UpdateAsync(user);
        }

        public async Task SendTwoFactorCodeAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var random = new Random();
            var randomCode = new string(Enumerable.Range(0, 6).Select(_ => chars[random.Next(chars.Length)]).ToArray());

            user.TwoFactorCode = randomCode;
            user.TwoFactorCodeDuration = DateTime.Now.AddMinutes(10);

            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
                throw new Exception("Failed");

            var emailContent = $"Your two-factor authentication code is {randomCode}. It will expire in 10 minutes.";

            var emailToSend = new Email
            {
                To = user.Email,
                Subject = "Two-factor authentication code",
                Body = emailContent
            };

            bool emailSent = await _emailService.SendEmail(emailToSend);

            if (!emailSent)
            {
                throw new Exception("Email sending failed.");
            }

        }
    }
}

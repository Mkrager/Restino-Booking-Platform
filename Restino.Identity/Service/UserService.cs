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
        private readonly IEmailService _emailService;
        private readonly ICodeGeneratorService _codeGeneratorService;
        public UserService(UserManager<ApplicationUser> userManager, IEmailService emailService, ICodeGeneratorService codeGeneratorService)
        {
            _userManager = userManager;
            _emailService = emailService;
            _codeGeneratorService = codeGeneratorService;
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
                throw new Exception("User not found.");

            var code = _codeGeneratorService.GenerateCode(6);
            user.Code = code;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded) 
                throw new Exception("Failed to update user code.");

            var emailSent = await _emailService.SendPasswordResetCode(email, code);
            if (!emailSent) 
                throw new Exception("Email sending failed.");
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

            user.TwoFactorEnabled = false;

            var updateResult = await _userManager.UpdateAsync(user);
        }

        public async Task SendTwoFactorCodeAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new Exception("User not found.");

            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
                throw new Exception("Failed");

            var code = _codeGeneratorService.GenerateCode(6);

            bool emailSent = await _emailService.SendTwoFactorCode(email, code);

            if (!emailSent)
                throw new Exception("Email sending failed.");
        }
    }
}
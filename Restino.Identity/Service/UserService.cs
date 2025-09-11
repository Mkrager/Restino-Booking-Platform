using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restino.Application.Contracts.Identity;
using Restino.Application.DTOs.Authentication;
using Restino.Identity.Models;

namespace Restino.Identity.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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

        public async Task ResetPasswordAsync(string email, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                throw new Exception("User not found.");

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            var resetResult = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);

            if (!resetResult.Succeeded)
            {
                throw new Exception("Password reset failed: " +
                    string.Join(", ", resetResult.Errors.Select(e => e.Description)));
            }
        }

        public async Task AddTwoFactorToAccountAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            user.TwoFactorEnabled = true;

            var updateResult = await _userManager.UpdateAsync(user);
        }

        public async Task DeleteTwoFactorFromAccountAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            user.TwoFactorEnabled = false;

            var updateResult = await _userManager.UpdateAsync(user);
        }
    }
}
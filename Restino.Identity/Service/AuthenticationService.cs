using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Restino.Application.Contracts.Identity;
using Restino.Application.DTOs.Authentication;
using Restino.Identity.Models;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Restino.Identity.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IUserService _userService;

        public AuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings, IUserService userService)
        {
            _jwtSettings = jwtSettings.Value;
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
        }
        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new Exception($"Password or email invalid.");
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                if (result.RequiresTwoFactor)
                {
                    await _userService.SendTwoFactorCodeAsync(user.Email);

                    return new AuthenticationResponse
                    {
                        Id = user.Id,
                        Email = user.Email,
                        UserName = user.UserName,
                        TwoFactorRequired = true
                    };
                }

                throw new Exception($"Password or email invalid.");
            }

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            AuthenticationResponse response = new AuthenticationResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                UserName = user.UserName
            };

            user.Code = null;

            await _userManager.UpdateAsync(user);

            return response;
        }

        public async Task<AuthenticationResponse> VerifyTwoFactorCodeAsync(VerifyCodeRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null || request.TwoFactorCode != user.TwoFactorCode)
            {
                throw new Exception("Invalid email or code.");
            }

            if (user.TwoFactorCodeDuration < DateTime.Now)
            {
                throw new Exception("Code duration end.");
            }

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            AuthenticationResponse response = new AuthenticationResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                UserName = user.UserName
            };

            return response;
        }


        public async Task<RegistrationResponse> RegisterAsync(RegistrationRequest request)
        {
            if (string.IsNullOrEmpty(request.Password))
            {
                throw new Exception("Password cannot be empty.");
            }

            var existingUser = await _userManager.FindByNameAsync(request.UserName);

            if (existingUser != null)
            {
                throw new Exception($"Username {request.UserName} already exists.");
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                EmailConfirmed = true,
            };

            var existingEmail = await _userManager.FindByEmailAsync(request.Email);

            if (existingEmail == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    return new RegistrationResponse() { UserId = user.Id };
                }
                else
                {
                    var errors = result.Errors.Select(e => e.Description).ToList();
                    throw new Exception($"{result.Errors}");
                }
            }
            else
            {
                throw new Exception($"Email {request.Email} already exists.");
            }
        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
                new Claim("EnabledTwoFactorAuth", user.TwoFactorEnabled.ToString())
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
    }
}

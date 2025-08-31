﻿using Restino.Application.Contracts;
using System.Security.Claims;

namespace Restino.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string UserId => _contextAccessor.HttpContext?.User?.FindFirstValue("uid");
        public string UserRole => _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
    }
}

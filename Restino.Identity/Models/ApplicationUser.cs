﻿using Microsoft.AspNetCore.Identity;

namespace Restino.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Code { get; set; }
    }
}
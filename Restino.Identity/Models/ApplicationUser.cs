using Microsoft.AspNetCore.Identity;
using Restino.Domain.Common;

namespace Restino.Identity.Models
{
    public class ApplicationUser : IdentityUser, IHasCode
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Code { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
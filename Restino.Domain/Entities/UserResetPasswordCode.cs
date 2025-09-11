using Restino.Domain.Common;

namespace Restino.Domain.Entities
{
    public class UserResetPasswordCode : AuditableEntity, IHasCode
    {
        public string? Code { get; set; }
        public string Email { get; set; } = string.Empty;
        public DateTime ExpirationTime { get; set; }
    }
}
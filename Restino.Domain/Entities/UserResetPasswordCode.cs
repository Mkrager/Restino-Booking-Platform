using Restino.Domain.Common;

namespace Restino.Domain.Entities
{
    public class UserResetPasswordCode : AuditableEntity, IHasCode
    {
        public string? Code { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
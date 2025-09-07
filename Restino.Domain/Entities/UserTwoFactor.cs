using Restino.Domain.Common;

namespace Restino.Domain.Entities
{
    public class UserTwoFactor : AuditableEntity
    {
        public string Code { get; set; } = string.Empty;
        public DateTime Duration { get; set; }
    }
}
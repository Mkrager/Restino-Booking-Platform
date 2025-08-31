using Restino.Application.Contracts.Application;
using Restino.Domain.Common;

namespace Restino.Application.Services
{
    public class PermissionService : IPermissionService
    {
        public bool HasUserPermission(AuditableEntity? entity, string userId, string userRole)
        {
            if (entity == null)
                return false;
            return entity.CreatedBy == userId || userRole == "Admin";
        }
    }
}

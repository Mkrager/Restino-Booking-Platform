using Restino.Domain.Common;

namespace Restino.Application.Contracts.Application
{
    public interface IPermissionService
    {
        bool HasUserPermission(AuditableEntity? entity, string userId, string userRole);
    }
}
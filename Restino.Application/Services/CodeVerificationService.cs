using Restino.Application.Contracts.Application;
using Restino.Domain.Common;

namespace Restino.Application.Services
{
    public class CodeVerificationService<T> : ICodeVerificationService<T> where T : IHasCode
    {
        public bool VerifyCode(T entity, string code)
        {
            return entity.Code == code
                && entity.ExpirationTime > DateTime.UtcNow;
        }
    }
}
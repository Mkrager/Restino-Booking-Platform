using Restino.Application.Contracts.Application;
using Restino.Domain.Common;

namespace Restino.Application.Services
{
    public class CodeVerificationService<T> : ICodeVerificationService<T> where T : IHasCode
    {
        public bool VerifyCodeAsync(T entity, string code)
        {
            if (entity.Code == code)
                return true;

            return false;
        }
    }
}
using Restino.Domain.Common;

namespace Restino.Application.Contracts.Application
{
    public interface ICodeVerificationService<T> where T : IHasCode
    {
        bool VerifyCodeAsync(T entity, string code);
    }
}

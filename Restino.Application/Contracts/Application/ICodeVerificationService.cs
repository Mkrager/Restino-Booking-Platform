using Restino.Domain.Common;

namespace Restino.Application.Contracts.Application
{
    public interface ICodeVerificationService<T> where T : IHasCode
    {
        bool VerifyCode(T entity, string code);
    }
}

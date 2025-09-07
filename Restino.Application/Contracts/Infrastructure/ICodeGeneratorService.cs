namespace Restino.Application.Contracts.Infrastructure
{
    public interface ICodeGeneratorService
    {
        string GenerateCode(int length = 6);
    }
}

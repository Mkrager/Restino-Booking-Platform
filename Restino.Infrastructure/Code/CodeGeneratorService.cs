using Restino.Application.Contracts.Infrastructure;

namespace Restino.Infrastructure.Code
{
    public class CodeGeneratorService : ICodeGeneratorService
    {
        private static readonly string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private readonly Random _random = new Random();

        public string GenerateCode(int length = 6)
        {
            return new string(Enumerable.Range(0, length)
                .Select(_ => Chars[_random.Next(Chars.Length)])
                .ToArray());
        }
    }
}
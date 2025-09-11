using MediatR;
using Restino.Application.Contracts.Identity;
using Restino.Application.Contracts.Infrastructure;
using Restino.Domain.Entities;

namespace Restino.Application.Features.User.Queries.SendTwoFactoreCode
{
    public class SendTwoFactoreCodeQueryHandler : IRequestHandler<SendTwoFactoreCodeQuery>
    {
        private readonly ICodeGeneratorService _codeGeneratorService;
        private readonly IEmailService _emailService;
        private readonly IAsyncIdentityRepository<UserTwoFactorCode> _userTwoFactorRepository;
        public SendTwoFactoreCodeQueryHandler(
            ICodeGeneratorService codeGeneratorService,
            IEmailService emailService,
            IAsyncIdentityRepository<UserTwoFactorCode> userTwoFactorRepository)
        {
            _codeGeneratorService = codeGeneratorService;
            _emailService = emailService;
            _userTwoFactorRepository = userTwoFactorRepository;
        }

        public async Task<Unit> Handle(SendTwoFactoreCodeQuery request, CancellationToken cancellationToken)
        {
            var code = _codeGeneratorService.GenerateCode(6);

            await _emailService.SendTwoFactorCode(request.Email, code);

            await _userTwoFactorRepository.AddAsync(new UserTwoFactorCode()
            {
                Code = code,
                ExpirationTime = DateTime.UtcNow.AddMinutes(10)
            });

            return Unit.Value;
        }
    }
}

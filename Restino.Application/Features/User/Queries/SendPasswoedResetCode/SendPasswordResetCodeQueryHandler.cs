using MediatR;
using Restino.Application.Contracts.Identity;
using Restino.Application.Contracts.Infrastructure;
using Restino.Domain.Entities;

namespace Restino.Application.Features.User.Queries.SendPasswoedResetCode
{
    internal class SendPasswordResetCodeQueryHandler : IRequestHandler<SendPasswordResetCodeQuery, Unit>
    {
        private readonly ICodeGeneratorService _codeGeneratorService;
        private readonly IEmailService _emailService;
        private readonly IAsyncIdentityRepository<UserResetPasswordCode> _userUserResetPasswordRepository;
        public SendPasswordResetCodeQueryHandler(
            ICodeGeneratorService codeGeneratorService,
            IEmailService emailService,
            IAsyncIdentityRepository<UserResetPasswordCode> userUserResetPasswordRepository)
        {
            _codeGeneratorService = codeGeneratorService;
            _emailService = emailService;
            _userUserResetPasswordRepository = userUserResetPasswordRepository;
        }

        public async Task<Unit> Handle(SendPasswordResetCodeQuery request, CancellationToken cancellationToken)
        {
            var code = _codeGeneratorService.GenerateCode(6);

            await _emailService.SendPasswordResetCode(request.Email, code);

            await _userUserResetPasswordRepository.AddAsync(new UserResetPasswordCode()
            {
                Code = code,
                ExpirationTime = DateTime.UtcNow.AddMinutes(10)
            });

            return Unit.Value;
        }
    }
}
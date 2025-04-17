using AutoMapper;
using MediatR;
using Restino.Application.Contracts.Identity;

namespace Restino.Application.Features.User.Queries.SendTwoFactoreCode
{
    public class SendTwoFactoreCodeQueryHandler : IRequestHandler<SendTwoFactoreCodeQuery>
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public SendTwoFactoreCodeQueryHandler(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<Unit> Handle(SendTwoFactoreCodeQuery request, CancellationToken cancellationToken)
        {
            await _userService.SendTwoFactorCodeAsync(request.Email);
            return Unit.Value;
        }
    }
}

//using AutoMapper;
//using MediatR;
//using Restino.Application.Contracts.Identity;

//namespace Restino.Application.Features.User.Queries.SendPasswoedResetCode
//{
//    internal class SendPasswordResetCodeQueryHandler : IRequestHandler<SendPasswordResetCodeQuery, Unit>
//    {
//        private readonly IMapper _mapper;
//        private readonly IUserService _userService;
//        public SendPasswordResetCodeQueryHandler(IMapper mapper, IUserService userService)
//        {
//            _mapper = mapper;
//            _userService = userService;
//        }

//        public async Task<Unit> Handle(SendPasswordResetCodeQuery request, CancellationToken cancellationToken)
//        {
//            await _userService.SendPasswordResetCodeAsync(request.Email);
//            return Unit.Value;
//        }
//    }
//}

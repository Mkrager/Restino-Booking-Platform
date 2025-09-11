﻿using MediatR;

namespace Restino.Application.Features.PasswordReset.Commands.ResetPassword
{
    public class ResetPasswordCommand : IRequest
    {
        public string Email { get; set; } = string.Empty;
        public string ResetPasswordCode { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}

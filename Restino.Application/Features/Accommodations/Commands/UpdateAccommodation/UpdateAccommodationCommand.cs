﻿using MediatR;

namespace Restino.Application.Features.Accommodations.Commands.UpdateAccommodation
{
    public class UpdateAccommodationCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ShortDescription { get; set; } = string.Empty;
        public string LongDescription { get; set; } = string.Empty;
        public string? ImgUrl { get; set; }
        public string Address { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public Guid CategoryId { get; set; }

        public string UserId { get; set; } = string.Empty;
        public string UserRole { get; set; } = string.Empty;
    }
}

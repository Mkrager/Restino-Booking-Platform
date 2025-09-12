using AutoMapper;
using Restino.Application.DTOs.Authentication;
using Restino.Application.Features.Accommodations.Commands.CreateAccommodation;
using Restino.Application.Features.Accommodations.Commands.DeleteAccommodation;
using Restino.Application.Features.Accommodations.Commands.UpdateAccommodation;
using Restino.Application.Features.Accommodations.Queries.GetAccommodationDetails;
using Restino.Application.Features.Accommodations.Queries.GetAccommodationList;
using Restino.Application.Features.Accounts.Commands.Registration;
using Restino.Application.Features.Accounts.Queries.Authentication;
using Restino.Application.Features.Categories.Commands.CreateCategoryCommand;
using Restino.Application.Features.Categories.Queries.GetCategoriesList;
using Restino.Application.Features.Categories.Queries.GetCategoriesListWithAccommodation;
using Restino.Application.Features.Categories.Queries.GetCategoryDetails;
using Restino.Application.Features.Reservations.Commands.CreateReservation;
using Restino.Application.Features.Reservations.Commands.DeleteReservation;
using Restino.Application.Features.Reservations.Queries.GetReservatioDetails;
using Restino.Application.Features.Reservations.Queries.GetReservationList;
using Restino.Application.Features.Reservations.Queries.GetUserReservations;
using Restino.Domain.Entities;

namespace Restino.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Accommodation, AccommodationListVm>().ReverseMap();
            CreateMap<Accommodation, AccommodationDetailsVm>().ReverseMap();
            CreateMap<Accommodation, CreateAccommodationCommand>().ReverseMap();
            CreateMap<Accommodation, UpdateAccommodationCommand>().ReverseMap();
            CreateMap<Accommodation, DeleteAccommodationCommand>().ReverseMap();
            CreateMap<Accommodation, CategoryAccommodationDto>().ReverseMap();

            CreateMap<Accommodation, AccommodationDtoReservation>()
                .ForMember(dest => dest.AccommodationName, opt => opt.MapFrom(src => src.Name)).ReverseMap();

            CreateMap<Category, CategoryDetailsVm>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryDtoAccommodation>().ReverseMap();
            CreateMap<Category, CategoryListVm>().ReverseMap();
            CreateMap<Category, CategoryAccommodationListVm>().ReverseMap();
            CreateMap<Category, CreateCategoryCommand>().ReverseMap();

            CreateMap<Reservation, CreateReservationCommand>().ReverseMap();
            CreateMap<Reservation, ReservationListVm>().ReverseMap();
            CreateMap<Reservation, ReservationDetailsVm>().ReverseMap();
            CreateMap<Reservation, DeleteReservationCommand>().ReverseMap();
            CreateMap<Reservation, GetUserReservationListVm>().ReverseMap();

            CreateMap<RegistrationRequest, RegistrationCommand>().ReverseMap();
            CreateMap<AuthenticationRequest, AuthenticationQuery>().ReverseMap();
            CreateMap<AuthenticationResponse, AuthenticationVm>().ReverseMap();
        }
    }
}
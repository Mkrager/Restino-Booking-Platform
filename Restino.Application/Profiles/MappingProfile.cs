using AutoMapper;
using Restino.Application.Features.Accommodations.Commands.CreateAccommodation;
using Restino.Application.Features.Accommodations.Commands.DeleteAccommodation;
using Restino.Application.Features.Accommodations.Commands.UpdateAccommodation;
using Restino.Application.Features.Accommodations.Queries.GetAccommodationDetails;
using Restino.Application.Features.Accommodations.Queries.GetAccommodationList;
using Restino.Application.Features.Accommodations.Queries.GetUserAccommodationList;
using Restino.Application.Features.Accommodations.Queries.SearchAccommodationList;
using Restino.Application.Features.Categories.Commands.CreateCategoryCommand;
using Restino.Application.Features.Categories.Queries.GetCategoriesList;
using Restino.Application.Features.Categories.Queries.GetCategoriesListWithAccommodation;
using Restino.Application.Features.Categories.Queries.GetCategoryDetails;
using Restino.Application.Features.Reservation.Commands.CreateReservation;
using Restino.Application.Features.Reservation.Commands.DeleteReservation;
using Restino.Application.Features.Reservation.Queries.GetReservatioDetails;
using Restino.Application.Features.Reservation.Queries.GetReservationList;
using Restino.Application.Features.Reservation.Queries.GetUserReservations;
using Restino.Application.Features.Reservation.Queries.ListUserReservations;
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
            CreateMap<Accommodation, SearchAccommodationListVm>().ReverseMap();
            CreateMap<Accommodation, UserAccommodationListVm>().ReverseMap();

            CreateMap<Accommodation, AccommodationDtoReservation>()
                .ForMember(dest => dest.AccommodationName, opt => opt.MapFrom(src => src.Name));

            CreateMap<Category, CategoryDetailsVm>().ReverseMap();
            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategoryDtoAccommodationSearch>();
            CreateMap<Category, CategoryUserDtoAccommodation>();
            CreateMap<Category, CategoryDtoAccommodation>();
            CreateMap<Category, CategoryListVm>();
            CreateMap<Category, CategoryAccommodationListVm>();
            CreateMap<Category, CreateCategoryCommand>();

            CreateMap<Reservations, CreateReservationCommand>().ReverseMap();
            CreateMap<Reservations, ReservationListVm>().ReverseMap();
            CreateMap<Reservations, ReservationDetailsVm>().ReverseMap();
            CreateMap<Reservations, DeleteReservationCommand>().ReverseMap();
            CreateMap<Reservations, GetUserReservationListVm>().ReverseMap();
        }
    }
}

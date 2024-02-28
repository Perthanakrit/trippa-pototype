using Core.Interface.Services;
using Core.Services;
using Domain.Entities;

namespace Core.Mapping
{
    public class TripMapping : AutoMapper.Profile
    {
        public TripMapping()
        {
            CreateMap<CustomTripServiceInput, Trip>();
            CreateMap<CustomTrip, CustomTrip>();
            CreateMap<TripServiceInput, Trip>();
            CreateMap<TripUpdateInput, Trip>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
                .ForMember(d => d.TypeOfTripId, opt => opt.MapFrom(s => s.TypeOfTripId))
                .ForMember(d => d.Landmark, opt => opt.MapFrom(s => s.Landmark))
                .ForMember(d => d.Duration, opt => opt.MapFrom(s => s.Duration))
                .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Price))
                .ForMember(d => d.Fee, opt => opt.MapFrom(s => s.Fee))
                .ForMember(d => d.Origin, opt => opt.MapFrom(s => s.Origin))
                .ForMember(d => d.Destination, opt => opt.MapFrom(s => s.Destination))
                .ForMember(d => d.MaxAttendees, opt => opt.MapFrom(s => s.MaxAttendee));

            CreateMap<CustomTrip, CustomTripAndTrip>();

            CreateMap<CustomTripAndTrip, CustomTrip>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Trip, opt => opt.MapFrom(s => s.Trip))
                .ForMember(d => d.TripId, opt => opt.MapFrom(s => s.Trip.Id))
                .ForMember(d => d.CreatedAt, opt => opt.MapFrom(s => s.Trip.CreatedAt))
                .ForMember(d => d.UpdatedAt, opt => opt.MapFrom(s => s.Trip.UpdatedAt))
                .ForMember(d => d.IsActive, opt => opt.MapFrom(s => s.Trip.IsActive));

            CreateMap<Trip, TripServiceResponse>()
                .ForMember(d => d.Hostname, opt => opt.MapFrom(s => s.Attendee.FirstOrDefault(a => a.IsHost).ApplicationUser.UserName))
                .ForMember(d => d.TypeOfTrip, opt => opt.MapFrom(s => s.TypeOfTrip))
                .ForMember(d => d.Agenda, opt => opt.MapFrom(s => s.TripAgenda))
                .ForMember(d => d.Photos, opt => opt.MapFrom(s => s.Photos))
                .ForMember(d => d.Attendee, opt => opt.MapFrom(s => s.Attendee));

            CreateMap<TypeOfTrip, TypeOfTripDto>();
            CreateMap<TripAttendee, TripAttendeeDto>()
                .ForMember(d => d.DisplayName, opt => opt.MapFrom(s => s.ApplicationUser.DisplayName))
                .ForMember(d => d.Image, opt => opt.MapFrom(s => s.ApplicationUser.Image.Url))
                .ForMember(d => d.IsHost, opt => opt.MapFrom(s => s.IsHost))
                .ForMember(d => d.IsAccepted, opt => opt.MapFrom(s => s.IsAccepted))
                .ForMember(d => d.Bio, opt => opt.MapFrom(s => s.ApplicationUser.Bio));

            CreateMap<TripAgenda, TripAgendaDto>()
                .ForMember(d => d.Time, opt => opt.MapFrom(s => s.Time));
            CreateMap<TripAgendaDto, TripAgenda>()
                .ForMember(d => d.Time, opt => opt.MapFrom(s => s.Time));

            CreateMap<TripPhoto, TripPhotoDto>();
            CreateMap<TripPhotoDto, TripPhoto>();

            CreateMap<CustomTripServiceInput, CustomTrip>()
                .ForMember(d => d.Trip, opt => opt.MapFrom(s => s));


            CreateMap<CustomTrip, CustomTripServiceResponse>()
                .ForMember(d => d.Trip, opt => opt.MapFrom(s => s.Trip));

        }
    }
}
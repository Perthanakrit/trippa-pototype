using Core.Interface.Services;
using Core.Services;
using Domain.Entities;

namespace Core.Mapping
{
    public class TripMapping : AutoMapper.Profile
    {
        public TripMapping()
        {
            CreateMap<Trip, TripServiceResponse>()
                .ForMember(d => d.Hostname, opt => opt.MapFrom(s => s.Attendee.FirstOrDefault(a => a.IsHost).ApplicationUser.UserName))
                .ForMember(d => d.TypeOfTrip, opt => opt.MapFrom(s => s.TypeOfTrip))
                .ForMember(d => d.Agenda, opt => opt.MapFrom(s => s.TripAgenda))
                .ForMember(d => d.Photos, opt => opt.MapFrom(s => s.Photos))
                .ForMember(d => d.Attendee, opt => opt.MapFrom(s => s.Attendee));

            CreateMap<TypeOfTrip, TypeOfTripDto>();
            CreateMap<TripAttendee, TripAttendeeDto>();
            CreateMap<TripAgenda, TripAgendaDto>();
            CreateMap<TripPhoto, TripPhotoDto>();
        }
    }
}
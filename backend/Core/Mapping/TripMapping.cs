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
            // Map attendee to tripservice response

            CreateMap<CustomTrip, CustomTripAndTrip>()
                .ForMember(d => d.CustomTripId, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Trip, opt => opt.MapFrom(s => s.Trip));

            CreateMap<Trip, TripServiceResponse>()
                .ForMember(d => d.Attendee, opt => opt.MapFrom(s => s.Attendee.Select(x => x.ApplicationUser)))
                .ForMember(d => d.Hostname, opt => opt.MapFrom(s => s.Attendee.FirstOrDefault(x => x.IsHost).ApplicationUser.DisplayName))
                .ForMember(d => d.Agenda, opt => opt.MapFrom(s => s.TripAgenda))
                .ForMember(d => d.TypeOfTrip, opt => opt.MapFrom(s => s.TypeOfTrip))
                .ForMember(d => d.Photos, opt => opt.MapFrom(s => s.Photos));



            CreateMap<TypeOfTrip, TypeOfTripDto>();

            CreateMap<TripAgenda, TripAgendaDto>();
            CreateMap<TripPhoto, TripPhotoDto>();
        }
    }
}
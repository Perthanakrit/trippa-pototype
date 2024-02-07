using Core.Interface.Services;
using Core.Services;
using Domain.Entities;

namespace Core.Mapping
{
    public class CustomTripMapping : AutoMapper.Profile
    {
        public CustomTripMapping()
        {
            CreateMap<CustomTripServiceInput, Trip>();
            // Map attendee to tripservice response
            CreateMap<Trip, TripServiceResponse>()
                .ForMember(d => d.Attendee, opt => opt.MapFrom(s => s.Attendee.Select(x => x.ApplicationUser)))
                .ForMember(d => d.Hostname, opt => opt.MapFrom(s => s.Attendee.FirstOrDefault(x => x.IsHost).ApplicationUser.DisplayName));

            CreateMap<CustomTrip, CustomTripAndTrip>()
                .ForMember(d => d.CustomTripId, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Trip, opt => opt.MapFrom(s => s.Trip));

        }
    }
}
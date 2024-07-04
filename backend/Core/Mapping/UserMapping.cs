using Core.Interface.Services;
using Core.Services;
using Domain.Entities;

namespace Core.Mapping
{
    public class UserMapping : AutoMapper.Profile
    {
        public UserMapping()
        {
            CreateMap<ApplicationUser, TripAttendeeDto>()
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.DisplayName))
                .ForMember(d => d.Bio, o => o.MapFrom(s => s.Bio))
                .ForMember(d => d.Image, o => o.MapFrom(s => s.Image))
                .ForMember(d => d.IsHost, o => o.MapFrom(s => s.AttendeedTrips.Where(x => x.IsHost).Any()))
                .ForMember(d => d.IsAccepted, o => o.MapFrom(s => s.AttendeedTrips.Where(x => x.IsAccepted).Any()))
                .ForMember(d => d.Contacts, o => o.MapFrom(s => s.Contacts.Select(x => x)));

            CreateMap<Contact, ContactDto>()
                .ForMember(d => d.Channel, o => o.MapFrom(s => s.Channel))
                .ForMember(d => d.Value, o => o.MapFrom(s => s.Name));
        }
    }
}
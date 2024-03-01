using Core.Interface.Services;
using Domain.Entities;

namespace Core.Mapping
{
    public class CommunityTripMapping : AutoMapper.Profile
    {
        public CommunityTripMapping()
        {
            CreateMap<CommunityTrip, CommuTripResponse>()
                .ForMember(d => d.Attendees, opt => opt.MapFrom(s => s.Attendees))
                .ForMember(d => d.Appointment, opt => opt.MapFrom(s => s.Appointment))
                .ForMember(d => d.Photos, opt => opt.MapFrom(s => s.Photos));

            CreateMap<CommunityTripAttendee, CommuTripAttendeeDto>()
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.ApplicationUser.UserName))
                .ForMember(d => d.UserPhoto, opt => opt.MapFrom(s => s.ApplicationUser.Image.Url))
                .ForMember(d => d.IsHost, opt => opt.MapFrom(s => s.IsHost))
                .ForMember(d => d.IsAccepted, opt => opt.MapFrom(s => s.IsAccepted));

            CreateMap<CommunityTripAppointment, CommuTripAppointmentDto>();

            CreateMap<CommunityTripPhoto, CommuTripPhoto>()
                .ForMember(d => d.Url, opt => opt.MapFrom(s => s.Url));

            CreateMap<CommuTripAppointmentInput, CommunityTripAppointment>()
                .ForMember(d => d.Date, opt => opt.MapFrom(s => s.Date))
                .ForMember(d => d.Time, opt => opt.MapFrom(s => new TimeOnly(s.TimeHour, s.TimeMinute)))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description));
        }
    }
}
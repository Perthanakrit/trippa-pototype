using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Core.Interface.Services
{
    public interface ICommunityTripService
    {
        Task CreateNewTripAsync(CommuTripInput input);
        Task UpdateTripAsync(Guid id, CommuTripInput input);
        Task DeleteTripAsync(Guid id);
        Task<CommuTripResponse> GetTripAsync(Guid id);
        Task<IEnumerable<CommuTripResponse>> GetListOfAllTripsAsync();
        Task UpdateAttendeeAsync(Guid tripId);
        Task AcceptAttendeeAsync(Guid tripId, MailInput email);
        Task RejectAttendeeAsync(Guid tripId, MailInput email);
        Task UploadPhotoAsync(Guid tripId, IFormFile file);
    }

    public class CommuTripResponse
    {
        public Guid Id { get; set; }
        public string Location { get; set; }
        public string Duration { get; set; }
        public string AgeRange { get; set; }
        public int MaxAttendees { get; set; }
        public CommuTripAppointmentDto Appointment { get; set; }
        public ICollection<CommuTripAttendeeDto> Attendees { get; set; } = new List<CommuTripAttendeeDto>();
        public ICollection<CommuTripPhoto> Photos { get; set; } = new List<CommuTripPhoto>();
        public bool IsActive { get; set; }
    }

    public class CommuTripInput
    {
        public string Location { get; set; }
        public string Duration { get; set; }
        public string AgeRange { get; set; }
        public int MaxAttendees { get; set; }
        public CommuTripAppointmentInput Appointment { get; set; }
    }

    public class CommuTripPhoto
    {
        public string Url { get; set; }
    }

    public class CommuTripAppointmentDto
    {
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public string Description { get; set; }
    }

    public class CommuTripAppointmentInput
    {
        public DateOnly Date { get; set; }
        public int TimeHour { get; set; }
        public int TimeMinute { get; set; }
        public string Description { get; set; }
    }

    public class CommuTripAttendeeDto
    {
        public string UserName { get; set; }
        public string UserPhoto { get; set; }
        public bool IsHost { get; set; }
        public bool IsAccepted { get; set; }
    }

    public struct MailInput
    {
        public string Email { get; set; }
    }

}
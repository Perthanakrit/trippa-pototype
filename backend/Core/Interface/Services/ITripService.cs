using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Core.Services
{
    public interface ITripService
    {
        Task CreateNewTripAsync(TripServiceInput input);
        Task<TripServiceResponse> UpdateTripAsync(Guid provinceId, TripServiceInput input);
        Task<TripServiceResponse> DeleteTripAsync(Guid provinceId);
        Task<TripServiceResponse> GetTripAsync(Guid provinceId);
        Task<TripsServiceResponseWithPaging> GetListOfAllTripsAsync();
        Task UpdateAttendeeAsync(Guid tripId);
        Task AcceptAttendeeAsync(Guid tripId, string userId);
        Task RejectAttendeeAsync(Guid tripId, string userId);
        //Task<ProvinceServiceResponseWithPaging> GetListOfAllActiveProvincesAsync(int pageNumber, int pageSize);
    }

    public class TripServiceInput
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Landmark { get; set; }
        [Required]
        public string Duration { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public float Fee { get; set; }
        [Required]
        public string Origin { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required(ErrorMessage = "The maximum attendee is required")]
        public int MaxAttendee { get; set; }
        [Required]
        public List<TripAgendaDto> TripAgendas { get; set; }
        public List<TripPhotoDto> Photos { get; set; } = new List<TripPhotoDto>();
        [Required]
        public Guid TypeOfTripId { get; set; }
    }

    public class TripServiceResponse : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Landmark { get; set; }
        public string Duration { get; set; }
        public float Price { get; set; }
        public float Fee { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string Hostname { get; set; }
        public int MaxAttendee { get; set; }
        public TypeOfTripDto TypeOfTrip { get; set; }
        public ICollection<TripAgendaDto> Agenda { get; set; } = new List<TripAgendaDto>();
        public ICollection<TripPhotoDto> Photos { get; set; } = new List<TripPhotoDto>();
        public ICollection<TripAttendeeDto> Attendee { get; set; } = new List<TripAttendeeDto>();
    }

    public class TripsServiceResponseWithPaging
    {
        public int TotalRows { get; set; }
        //public int PageNumber { get; set; }
        //public int PageSize { get; set; }
        public List<TripServiceResponse> Trips { get; set; } = new List<TripServiceResponse>();
    }

    public class TripAttendeeDto
    {
        public string DisplayName { get; set; }
        public string Bio { get; set; }
        public string Image { get; set; }
        public bool IsHost { get; set; }
        public bool IsAccepted { get; set; }
        public ICollection<ContactDto> Contacts { get; set; }
    }

    public class ContactDto
    {
        public string Channel { get; set; }
        public string Value { get; set; }
    }

    public class TypeOfTripDto
    {
        public string Name { get; set; }
    }

    public class TripAgendaDto
    {
        public string Description { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
    }

    public class TripPhotoDto
    {
        public string Url { get; set; }
    }
}
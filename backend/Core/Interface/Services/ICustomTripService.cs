using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Services;
using Domain.Entities;

namespace Core.Interface.Services
{
    public interface ICustomTripService
    {
        Task CreateNewTripAsync(CustomTripServiceInput input);
        Task<CustomTripServiceResponse> UpdateTripAsync(Guid provinceId, CustomTripServiceInput input);
        Task DeleteTripAsync(Guid provinceId);
        Task<CustomTripAndTrip> GetTripAsync(Guid provinceId);
        Task<List<CustomTripAndTrip>> GetListOfAllTripsAsync();
    }

    // DTOs
    public class CustomTripServiceInput
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Landmark { get; set; }
        public string Duration { get; set; }
        public float Price { get; set; }
        public float Fee { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public int MaxAttendee { get; set; }
        public List<TripPhotoDto> Photos { get; set; }
        public List<TripAgendaDto> TripAgendas { get; set; }
        public Guid TypeOfTripId { get; set; }
    }

    public class CustomTripServiceResponse : BaseEntity
    {
        // public string Name { get; set; }
        // public string Description { get; set; }
        // public int Duration { get; set; }
        // public float Price { get; set; }
        // public string Origin { get; set; }
        // public string Destination { get; set; }
        public Trip Trip { get; set; }
        public Guid TripId { get; set; }
    }

    public class CustomTripServiceResponseWithTrip
    {
        public CustomTrip CustomTrip { get; set; }
        public Trip Trip { get; set; }

    }

    public class CustomTripAndTrip
    {
        public Guid CustomTripId { get; set; }
        public TripServiceResponse Trip { get; set; }
    }

    public class CustomTripsServiceResponseWithPaging
    {
        public int TotalRows { get; set; }
        //public int PageNumber { get; set; }
        //public int PageSize { get; set; }
        public List<CustomTripAndTrip> CustomTrips { get; set; } = new List<CustomTripAndTrip>();
    }
}
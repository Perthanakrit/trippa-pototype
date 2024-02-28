using Core.Services;
using Domain.Entities;

namespace Core.Interface.Services
{
    public interface ICustomTripService
    {
        Task CreateNewTripAsync(CustomTripServiceInput input);
        Task UpdateTripAsync(Guid provinceId, TripUpdateInput input);
        Task DeleteTripAsync(Guid provinceId);
        Task<CustomTripAndTrip> GetTripAsync(Guid provinceId);
        Task<List<CustomTripAndTrip>> GetListOfAllTripsAsync();
    }

    // DTOs
    public class CustomTripServiceInput : TripServiceInput
    {
    }

    public class CustomTripUpdateInput : TripUpdateInput
    {
    }

    public class CustomTripServiceResponse : BaseEntity
    {
        public Trip Trip { get; set; }
        // public Guid TripId { get; set; }
    }

    public class CustomTripServiceResponseWithTrip
    {
        public CustomTrip CustomTrip { get; set; }
        public Trip Trip { get; set; }

    }

    public class CustomTripAndTrip
    {
        public Guid Id { get; set; }
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
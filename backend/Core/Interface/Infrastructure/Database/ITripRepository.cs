using Core.Services;
using Domain.Entities;

namespace Core.Interface.Infrastructure.Database
{
    public interface ITripRepository : IBaseRepository<Trip>
    {
        Task<Trip> GetByIdCache(Guid id);
        Task<List<TripServiceResponse>> GetTripsAsync();
        Task<TripServiceResponse> GetTripAsync(Guid provinceId);
        Task<Trip> ExistedTripAsync(Guid tripId);
        Task AddAttendeeAsync(Trip trip);
        Task<List<TripAttendee>> GetAllAttendeeAsync(Guid tripId);
        Task<Trip> GetOneTrip(Guid tripId);
    }
}
using Core.Interface.Services;
using Domain.Entities;

namespace Core.Interface.Infrastructure.Database
{
    public interface ICustomTripRepository : IBaseRepository<CustomTrip>
    {
        Task<CustomTripAndTrip> GetTripByCustomTripId(Guid customTripId);
        Task<List<CustomTripAndTrip>> GetTripsInCustomTrips();
        Task DeleteIncludeTripAsync(Guid id);
        Task<CustomTrip> GetCustomTripById(Guid id);
    }
}
using Core.Interface.Services;
using Domain.Entities;

namespace Core.Interface.Infrastructure.Database
{
    public interface ICommunityTripRespository : IBaseRepository<CommunityTrip>
    {
        Task<CommuTripResponse> GetTripWithRelatedDataAsync(Guid tripId);
    }
}
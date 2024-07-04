namespace Domain.Entities
{
    public class CommunityTripPhoto : Photo
    {
        public CommunityTrip CommunityTrip { get; set; }
        public Guid CommunityTripId { get; set; }
    }
}
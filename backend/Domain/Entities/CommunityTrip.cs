namespace Domain.Entities
{
    public class CommunityTrip : BaseEntity, IBaseEntity
    {
        public string Location { get; set; }
        public string Duration { get; set; }
        public int MaxAttendees { get; set; }
        public string AgeRange { get; set; }
        public CommunityTripAppointment Appointment { get; set; }
        public ICollection<CommunityTripAttendee> Attendees { get; set; } = new List<CommunityTripAttendee>();
        public ICollection<CommunityTripPhoto> Photos { get; set; } = new List<CommunityTripPhoto>();

    }
}
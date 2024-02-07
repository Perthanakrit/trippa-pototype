namespace Domain.Entities
{
    /*
        public Guid Id { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public bool IsHost { get; set; }
        public DateTime AttendAt { get; set; }
        public DateTime? CancelAt { get; set; }
    */
    public class TripAttendee : Attendee
    {
        public Trip Trip { get; set; }
        public Guid TripId { get; set; }
    }
}
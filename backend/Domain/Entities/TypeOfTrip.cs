namespace Domain.Entities
{
    public class TypeOfTrip
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Trip> Trips { get; set; } // Navigation property
    }
}
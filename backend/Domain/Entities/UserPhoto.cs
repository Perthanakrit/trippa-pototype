namespace Domain.Entities
{
    public class UserPhoto : Photo
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
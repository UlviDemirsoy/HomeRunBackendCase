namespace NotificationService.Dtos
{
    public class RatingPublishedDto
    {
        public int Id { get; set; }
        public int Point { get; set; }
        public int ServiceProviderId { get; set; }
        public string Event { get; set; }
    }
}

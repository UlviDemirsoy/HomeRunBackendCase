namespace RatingService.Dtos
{
    public class ServiceProviderRatingReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double AverageRating { get; set; }

    }
}

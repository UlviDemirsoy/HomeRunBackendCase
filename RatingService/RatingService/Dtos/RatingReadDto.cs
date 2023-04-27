using System.ComponentModel.DataAnnotations;

namespace RatingService.Dtos
{
    public class RatingReadDto
    {
        public int Id { get; set; }
        public int Point { get; set; }
        public int ServiceProviderId { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RatingService.Models
{
    public class ServiceProvider
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    }
}

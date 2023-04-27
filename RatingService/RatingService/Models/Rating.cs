using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RatingService.Models
{
    public class Rating
    {
        [Key]
        [Required]
        public int Id { get; set; }
      
        [Required]
        public int Point { get; set; }

        [Required]
        public int ServiceProviderId { get; set; }
        public ServiceProvider ServiceProvider { get; set; }

    }
}

using RatingService.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RatingService.Dtos
{
    public class ServiceProviderReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }

}

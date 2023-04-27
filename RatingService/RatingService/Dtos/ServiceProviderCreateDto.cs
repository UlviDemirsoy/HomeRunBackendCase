using RatingService.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RatingService.Dtos
{
    public class ServiceProviderCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}

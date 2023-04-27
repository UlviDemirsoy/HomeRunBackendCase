using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace NotificationService.Models
{
    public class Rating
    {

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int ServiceProviderId { get; set; }

        [Required]
        public int Point { get; set; }

        public bool Ack { get; set; } = false;


    }
}

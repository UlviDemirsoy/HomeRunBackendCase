using AutoMapper;
using NotificationService.Dtos;
using NotificationService.Models;

namespace NotificationService.Profiles
{
    public class RatingsProfile : Profile
    {
        public RatingsProfile()
        {
            // Source -> Target
            CreateMap<Rating, RatingReadDto>();
            CreateMap<RatingPublishedDto, Rating>();


                //.ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.Id));
        }

    }
}

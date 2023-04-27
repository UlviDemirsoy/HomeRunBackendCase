using AutoMapper;
using RatingService.Dtos;
using RatingService.Models;

namespace RatingService.Profiles
{
    public class ServiceProvidersProfile : Profile
    {

        public ServiceProvidersProfile() 
        {
            //Source ->Target
            CreateMap<ServiceProvider, ServiceProviderReadDto>();
            CreateMap<ServiceProviderCreateDto, ServiceProvider>();

            CreateMap<Rating, RatingReadDto>(); 
            CreateMap<RatingCreateDto, Rating>();
            CreateMap<RatingReadDto, RatingPublishedDto>();
        }
    }
}

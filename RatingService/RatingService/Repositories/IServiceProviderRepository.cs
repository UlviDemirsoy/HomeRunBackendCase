using RatingService.Dtos;
using RatingService.Models;
using System.Collections.Generic;

namespace RatingService.Repositories
{
    public interface IServiceProviderRepository
    {
        bool SaveChanges();
        //ServiceProviders
        IEnumerable<ServiceProvider> GetAllServiceProviders();
        ServiceProvider GetServiceProviderById(int id);
        void CreateServiceProvider(ServiceProvider serviceProvider);
        IEnumerable<ServiceProviderRatingReadDto> GetAllServiceProvidersWithAverageRating();
        ServiceProviderRatingReadDto GetServiceProviderWithAverageRatingById(int id);
        bool ServiceProviderExists(int id);

        //Ratings
        void CreateRating(Rating rating);
        Rating GetRatingById(int id);
        IEnumerable<Rating> GetRatings();
        IEnumerable<Rating> GetRatingsForServiceProvider(int id);


    }
}

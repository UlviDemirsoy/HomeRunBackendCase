using RatingService.Dtos;

namespace RatingService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewRating(RatingPublishedDto ratingPublishedDto);
    }
}
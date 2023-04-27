using RatingService.Dtos;
using System.Threading.Tasks;

namespace RatingService.SyncDataServices.Http
{
    public interface ICommandDataClient
    {
        //publish dto olcak
        Task SendRatingToNotification(RatingReadDto rating); 

    }
}
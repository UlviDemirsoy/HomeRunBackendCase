using NotificationService.Models;
using System.Collections.Generic;

namespace NotificationService.Repositories
{
    public interface INotificationRepository
    {
        bool SaveChanges();
        IEnumerable<Rating> GetUnackedNotifications();
        void AckAllNotifications();
        void CreateNotification(Rating rating);

    }
}

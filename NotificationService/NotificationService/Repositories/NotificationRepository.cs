using NotificationService.Data;
using NotificationService.Models;
using System.Collections.Generic;
using System.Linq;

namespace NotificationService.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _context;

        public NotificationRepository(AppDbContext context)
        {
            _context = context;
        }
        public void AckAllNotifications()
        {
            var q = _context.Ratings.ToList();
            foreach(var item in q)
            {
                item.Ack = true;
            }
            
            _context.Ratings.UpdateRange(q);

        }

        public void CreateNotification(Rating rating)
        {
            _context.Ratings.Add(rating);
        }

        public IEnumerable<Rating> GetUnackedNotifications()
        {
            return _context.Ratings.Where(x => x.Ack == false).ToList();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}

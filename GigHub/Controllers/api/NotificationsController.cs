using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;


namespace GigHub.Controllers.api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();

            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();

            return notifications.Select(n => new NotificationDto()
            {
                DateTime = n.DateTime,
                Type = n.Type,
                OriginalDatetime = n.OriginalDatetime,
                OriginalVenue = n.OriginalVenue,
                Gig = new GigDto()
                {
                    DateTime = n.Gig.DateTime,
                    IsCanceled = n.Gig.IsCanceled,
                    Id = n.Gig.Id,
                    Venue = n.Gig.Venue,
                    Genre = new GenreDto()
                    {
                        Id = n.Gig.Genre.Id,
                        Name = n.Gig.Genre.Name
                    },
                    Artist = new UserDto()
                    {
                        Id = n.Gig.Artist.Id,
                        Name = n.Gig.Artist.Name
                    }
                }
            });
        }
    }
}
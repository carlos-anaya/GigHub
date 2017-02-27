using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GigHub.Models
{
    public class Gig
    {
        public int Id { get; set; }

        public bool IsCanceled { get; private set; }

        public ApplicationUser Artist { get; set; }

        [Required]
        public string ArtistId { get; set; }

        public DateTime DateTime { get; private set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; private set; }

        public Genre Genre { get; set; }

        [Required]
        public byte GenreId { get; private set; }

        public ICollection<Attendance> Attendances { get; private set; }

        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }

        public void Cancel()
        {
            IsCanceled = true;

            var notification = new Notification(NotificationType.GigCanceled, this);

            foreach (var attendee in Attendances.Select(a => a.Attendee))
                attendee.Notify(notification);
        }

        public void Modify(DateTime dateTime, byte genreId, string venue)
        {
            var notification = new Notification(NotificationType.GigUpdated, this);
            notification.OriginalDatetime = DateTime;
            notification.OriginalVenue = Venue;

            DateTime = dateTime;
            GenreId = genreId;
            Venue = venue;

            foreach (var attendee in Attendances.Select(a => a.Attendee))
                attendee.Notify(notification);
        }
    }
}
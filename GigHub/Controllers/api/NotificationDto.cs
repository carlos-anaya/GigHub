using GigHub.Models;
using System;

namespace GigHub.Controllers.api
{
    public class NotificationDto
    {
        public DateTime DateTime { get; set; }
        public NotificationType Type { get; set; }
        public string OriginalVenue { get; set; }
        public DateTime? OriginalDatetime { get; set; }
        public GigDto Gig { get; set; }
    }
}
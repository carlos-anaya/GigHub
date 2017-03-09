using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Persistence.Repositories
{
    public class GigRepository : IGigRepository
    {
        private readonly ApplicationDbContext _context;

        public GigRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Gig> GetGigsUserAttending(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
        }

        public Gig GetGigWithAttendees(int gigId)
        {
            return _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .SingleOrDefault(g => g.Id == gigId);
        }

        public IEnumerable<Gig> GetFutureGigs()
        {
            return _context.Gigs
                .Where(g => g.DateTime > DateTime.Now
                    && !g.IsCanceled)
                .Include(g => g.Genre)
                .Include(g => g.Artist)
                .ToList();
        }

        public IEnumerable<Gig> GetFutureGigsByArtist(string artistId)
        {
            return _context.Gigs
                .Where(g => g.ArtistId == artistId
                    && g.DateTime > DateTime.Now
                    && !g.IsCanceled)
                .Include(g => g.Genre)
                .ToList();
        }

        public IEnumerable<Gig> SearchGigs(string query)
        {
            var upcomingGigs = GetFutureGigs();

            if (!string.IsNullOrWhiteSpace(query))
                upcomingGigs = upcomingGigs
                    .Where(g => g.Artist.Name.Contains(query)
                        || g.Venue.Contains(query)
                        || g.Genre.Name.Contains(query));

            return upcomingGigs;
        }

        public Gig GetGig(int id)
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .SingleOrDefault(g => g.Id == id);
        }

        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);
        }
    }
}
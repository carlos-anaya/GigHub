using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.Repositories
{
    public interface IGigRepository
    {
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        Gig GetGigWithAttendees(int gigId);
        IEnumerable<Gig> GetFutureGigs();
        IEnumerable<Gig> GetFutureGigsByArtist(string artistId);
        Gig GetGig(int id);
        void Add(Gig gig);
    }
}
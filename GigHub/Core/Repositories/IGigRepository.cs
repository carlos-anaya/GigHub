using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface IGigRepository
    {
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        Gig GetGigWithAttendees(int gigId);
        IEnumerable<Gig> GetFutureGigs();
        IEnumerable<Gig> GetFutureGigsByArtist(string artistId);
        IEnumerable<Gig> SearchGigs(string query);
        Gig GetGig(int id);
        void Add(Gig gig);
    }
}
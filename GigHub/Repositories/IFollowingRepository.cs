using GigHub.Models;

namespace GigHub.Repositories
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string followeeId, string followerId);
        bool IsFollowing(string followeeId, string followerId);
    }
}
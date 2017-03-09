using GigHub.Repositories;

namespace GigHub.Persistence
{
    public interface IUnitOfWork
    {
        IAttendanceRepository Attendances { get; }
        IFollowingRepository Followings { get; }
        IGigRepository Gigs { get; }
        IGenreRepository Genres { get; }
        void Complete();
    }
}
using GigHub.Core.Repositories;

namespace GigHub.Core
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
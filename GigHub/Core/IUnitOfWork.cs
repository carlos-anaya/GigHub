using GigHub.Core.Repositories;

namespace GigHub.Core
{
    public interface IUnitOfWork
    {
        IApplicationUserRepository ApplicationUsers { get; }
        IAttendanceRepository Attendances { get; }
        IFollowingRepository Followings { get; }
        IGigRepository Gigs { get; }
        IGenreRepository Genres { get; }
        INotificationRepository Notifications { get; }
        IUserNotificationRepository UserNotifications { get; }
        void Complete();
    }
}
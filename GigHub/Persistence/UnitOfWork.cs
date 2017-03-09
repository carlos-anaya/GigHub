using GigHub.Core;
using GigHub.Core.Repositories;
using GigHub.Persistence.Repositories;

namespace GigHub.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IApplicationUserRepository ApplicationUsers { get; }
        public IAttendanceRepository Attendances { get; }
        public IFollowingRepository Followings { get; }
        public IGigRepository Gigs { get; }
        public IGenreRepository Genres { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            ApplicationUsers = new ApplicationUserRepository(context);
            Attendances = new AttendanceRepository(context);
            Followings = new FollowingRepository(context);
            Gigs = new GigRepository(context);
            Genres = new GenreRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

    }
}
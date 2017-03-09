using GigHub.Models;
using GigHub.Persistence;
using GigHub.Repositories;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly AttendanceRepository _attendanceRepository;
        private readonly FollowingRepository _followingRepository;
        private readonly GigRepository _gigRepository;
        private readonly GenreRepository _genreRepository;
        private readonly UnitOfWork _unitOfWork;

        public GigsController()
        {
            var context = new ApplicationDbContext();
            _attendanceRepository = new AttendanceRepository(context);
            _followingRepository = new FollowingRepository(context);
            _gigRepository = new GigRepository(context);
            _genreRepository = new GenreRepository(context);
            _unitOfWork = new UnitOfWork(context);
        }

        [Authorize]
        public ActionResult Mine()
        {
            var gigs = _gigRepository
                .GetFutureGigsByArtist(User.Identity.GetUserId());

            return View(gigs);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var viewModel = new GigsViewModel()
            {
                UpcomingGigs = _gigRepository.GetGigsUserAttending(userId),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending",
                Attendances = _attendanceRepository.GetFutureAttendances(userId)
                    .ToLookup(a => a.GigId)
            };

            return View("Gigs", viewModel);
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Id = 0,
                Genres = _genreRepository.GetGenres(),
                Heading = "Add a Gig"
            };

            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _genreRepository.GetGenres();
                return View("GigForm", viewModel);
            }

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _gigRepository.Add(gig);
            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var gig = _gigRepository.GetGig(id);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            var viewModel = new GigFormViewModel
            {
                Id = gig.Id,
                Heading = "Edit a Gig",
                Genres = _genreRepository.GetGenres(),
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue
            };

            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _genreRepository.GetGenres();
                return View("GigForm", viewModel);
            }

            var gig = _gigRepository.GetGigWithAttendees(viewModel.Id);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            gig.Modify(viewModel.GetDateTime(), viewModel.Genre, viewModel.Venue);

            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }

        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        public ActionResult Details(int id)
        {
            var gig = _gigRepository.GetGig(id);

            if (gig == null)
                return HttpNotFound();

            var viewModel = new GigDetailsViewModel()
            {
                Gig = gig
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                viewModel.IsGoing =
                    _attendanceRepository.IsAttending(id, userId);

                viewModel.IsFollowing =
                    _followingRepository.IsFollowing(gig.ArtistId, userId);
            }

            return View(viewModel);
        }
    }
}
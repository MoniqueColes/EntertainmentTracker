using BooksAndMovies.Models.MovieModels;
using BooksAndMovies.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;

namespace BooksAndMoviesMVC.Controllers
{
    [Authorize]
    public class MovieController : Controller
    {
        // GET: Movie
        public ActionResult Index()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new MovieService(userID);
            var model = service.GetMovies();

            return View(model);
        }

        //GET
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MovieCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateMovieService();

            if (service.CreateMovie(model))
            {
                TempData["SaveResult"] = "Your movie was added.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Movie could not be added.");

            return View(model);
        }

        public ActionResult Details(int ID)
        {
            var svc = CreateMovieService();
            var model = svc.GetMovieByID(ID);

            return View(model);
        }

        public ActionResult Edit(int ID)
        {
            var service = CreateMovieService();
            var detail = service.GetMovieByID(ID);
            var model =
                new MovieEdit
                {
                    MovieID = detail.MovieID,
                    Title = detail.Title,
                    MainCast = detail.MainCast,
                    Description = detail.Description,
                    YearReleased = detail.YearReleased,
                    DateWatched = detail.DateWatched,
                    IsRecommended = detail.IsRecommended,
                    ModifiedUtc = DateTimeOffset.UtcNow
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int ID, MovieEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.MovieID != ID)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateMovieService();

            if (service.UpdateMovie(model))
            {
                TempData["SaveResult"] = "Your movie was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your movie could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult DeleteMovie(int ID)
        {
            var svc = CreateMovieService();
            var model = svc.GetMovieByID(ID);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int ID)
        {
            var service = CreateMovieService();

            service.DeleteMovie(ID);

            TempData["SaveResult"] = "Your movie was deleted";

            return RedirectToAction("Index");
        }

        private MovieService CreateMovieService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new MovieService(userID);
            return service;
        }
    }
}
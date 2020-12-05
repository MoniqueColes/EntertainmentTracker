using BooksAndMovies.Models.ShowModels;
using BooksAndMovies.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;

namespace BooksAndMoviesMVC.Controllers
{
    [Authorize]
    public class ShowController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new ShowService(userID);
            var model = service.GetShows();

            return View(model);
        }

        //GET
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShowCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateShowService();

            if (service.CreateShow(model))
            {
                TempData["SaveResult"] = "Your show was added.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Show could not be added.");

            return View(model);
        }

        public ActionResult Details(int ID)
        {
            var svc = CreateShowService();
            var model = svc.GetShowByID(ID);

            return View(model);
        }

        public ActionResult Edit(int ID)
        {
            var service = CreateShowService();
            var detail = service.GetShowByID(ID);
            var model =
                new ShowEdit
                {
                    ShowID = detail.ShowID,
                    Title = detail.Title,
                    MainCast = detail.MainCast,
                    Description = detail.Description,
                    YearReleased = detail.YearReleased,
                    YearStarted = detail.YearStarted,
                    YearFinished = detail.YearFinished,
                    IsRecommended = detail.IsRecommended,
                    ModifiedUtc = DateTimeOffset.UtcNow
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int ID, ShowEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ShowID != ID)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateShowService();

            if (service.UpdateShow(model))
            {
                TempData["SaveResult"] = "Your show was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your show could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult DeleteShow(int ID)
        {
            var svc = CreateShowService();
            var model = svc.GetShowByID(ID);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int ID)
        {
            var service = CreateShowService();

            service.DeleteShow(ID);

            TempData["SaveResult"] = "Your show was deleted";

            return RedirectToAction("Index");
        }

        private ShowService CreateShowService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new ShowService(userID);
            return service;
        }
    }
}
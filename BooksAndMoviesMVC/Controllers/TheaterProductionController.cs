using BooksAndMovies.Models.TheaterProductionModels;
using BooksAndMovies.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;

namespace BooksAndMoviesMVC.Controllers
{
    [Authorize]
    public class TheaterProductionController : Controller
    {
        // GET: TheaterProduction
        public ActionResult Index()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new TheaterProductionService(userID);
            var model = service.GetTheaterProductions();

            return View(model);
        }

        //GET
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TheaterProductionCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateTheaterProductionService();

            if (service.CreateTheaterProduction(model))
            {
                TempData["SaveResult"] = "Your theater production was added.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Theater production could not be added.");

            return View(model);
        }

        public ActionResult Details(int ID)
        {
            var svc = CreateTheaterProductionService();
            var model = svc.GetTheaterProductionByID(ID);

            return View(model);
        }

        public ActionResult Edit(int ID)
        {
            var service = CreateTheaterProductionService();
            var detail = service.GetTheaterProductionByID(ID);
            var model =
                new TheaterProductionEdit
                {
                    TheaterProductionID = detail.TheaterProductionID,
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
        public ActionResult Edit(int ID, TheaterProductionEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.TheaterProductionID != ID)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateTheaterProductionService();

            if (service.UpdateTheaterProduction(model))
            {
                TempData["SaveResult"] = "Your theater production was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your theater production could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult DeleteTheaterProduction(int ID)
        {
            var svc = CreateTheaterProductionService();
            var model = svc.GetTheaterProductionByID(ID);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int ID)
        {
            var service = CreateTheaterProductionService();

            service.DeleteTheaterProduction(ID);

            TempData["SaveResult"] = "Your theater production was deleted";

            return RedirectToAction("Index");
        }

        private TheaterProductionService CreateTheaterProductionService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new TheaterProductionService(userID);
            return service;
        }
    }
}
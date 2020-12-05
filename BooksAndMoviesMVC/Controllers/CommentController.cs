using BooksAndMovies.Models.CommentModels;
using BooksAndMovies.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;

namespace BooksAndMoviesMVC.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        // GET: Comment
        public ActionResult Index()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new CommentService(userID);
            var model = service.GetComments();

            return View(model);
        }

        public ActionResult Create()
        {
            var db = new BookService();
            ViewBag.BookID = new SelectList(db.GetBooks().OrderBy(e => e.Title), "BookID", "Title");
            return View();
        }

        //var db = new MovieService();
        //ViewBag.MovieID = new SelectList(db.GetMovies().OrderBy(e => e.Title), "MovieID", "Title");
           // var db = new ShowService();
          //  ViewBag.ShowID = new SelectList(db.GetShows().OrderBy(e => e.Title), "ShowID", "Title");
          //  var db = new TheaterProductionService();
         //   ViewBag.TheaterProductionID = new SelectList(db.GetTheaterProductions().OrderBy(e => e.Title), "TheaterProductionID", "Title");

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CommentCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateCommentService();

            if (service.CreateComment(model))
            {
                TempData["SaveResult"] = "Your comment was added.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Comment could not be added.");

            return View(model);
        }

        public ActionResult Details(int ID)
        {
            var svc = CreateCommentService();
            var model = svc.GetCommentByID(ID);
            var db = new BookService();
            ViewBag.BookID = new SelectList(db.GetBooks().OrderBy(e => e.Title), "BookID", "Title");

            return View(model);
        }

        public ActionResult Edit(int ID)
        { 
            var service = CreateCommentService();
            var detail = service.GetCommentByID(ID);
            var model =
                new CommentEdit
                {
                    CommentID = detail.CommentID,
                    //BookID = detail.BookID,
                    //Book = detail.Book,
                    //MovieID = detail.MovieID,
                    //Movie = detail.Movie,
                   // ShowID = detail.ShowID,
                   // Show = detail.Show,
                   // TheaterProductionID = detail.TheaterProductionID,
                   // TheaterProducton = detail.TheaterProduction,
                    TypeOfMedia = detail.TypeOfMedia,
                    MyComment = detail.MyComment,
                    IsRecommended = detail.IsRecommended,
                    ModifiedUtc = DateTimeOffset.UtcNow
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int ID, CommentEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.CommentID != ID)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateCommentService();

            if (service.UpdateComment(model))
            {
                TempData["SaveResult"] = "Your comment was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your comment could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult DeleteComment(int ID)
        {
            var svc = CreateCommentService();
            var model = svc.GetCommentByID(ID);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int ID)
        {
            var service = CreateCommentService();

            service.DeleteComment(ID);

            TempData["SaveResult"] = "Your comment was deleted";

            return RedirectToAction("Index");
        }

        private CommentService CreateCommentService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new CommentService(userID);
            return service;
        }
    }
}
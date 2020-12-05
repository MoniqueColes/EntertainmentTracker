using BooksAndMovies.Models.BookModels;
using BooksAndMovies.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;

namespace BooksAndMoviesMVC.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new BookService(userID);
            var model = service.GetBooks();

            return View(model);
        }

        //GET
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateBookService();

            if (service.CreateBook(model))
            {
                TempData["SaveResult"] = "Your book was added.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Book could not be added.");

            return View(model);
        }

        public ActionResult Details(int ID)
        {
            var svc = CreateBookService();
            var model = svc.GetBookByID(ID);

            return View(model);
        }

        public ActionResult Edit(int ID)
        {
            var service = CreateBookService();
            var detail = service.GetBookByID(ID);
            var model =
                new BookEdit
                {
                    BookID = detail.BookID,
                    Title = detail.Title,
                    Author = detail.Author,
                    Description = detail.Description,
                    YearPublished = detail.YearPublished,
                    DateRead = detail.DateRead,
                    IsRecommended = detail.IsRecommended,
                    ModifiedUtc = DateTimeOffset.UtcNow
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int ID, BookEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.BookID != ID)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateBookService();

            if (service.UpdateBook(model))
            {
                TempData["SaveResult"] = "Your book was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your book could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult DeleteBook(int ID)
        {
            var svc = CreateBookService();
            var model = svc.GetBookByID(ID);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int ID)
        {
            var service = CreateBookService();

            service.DeleteBook(ID);

            TempData["SaveResult"] = "Your book was deleted";

            return RedirectToAction("Index");
        }

        private BookService CreateBookService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new BookService(userID);
            return service;
        }
    }
}
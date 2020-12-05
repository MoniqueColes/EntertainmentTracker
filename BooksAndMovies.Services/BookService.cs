using BooksAndMovies.Data;
using BooksAndMovies.Models.BookModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BooksAndMovies.Services
{
    public class BookService
    {
        private readonly Guid _userID;

        public BookService(Guid userID)
        {
            _userID = userID;
        }

        public BookService()
        {

        }

        public bool CreateBook(BookCreate model)
        {
            var entity =
                new Book()
                {
                    OwnerID = _userID,
                    Title = model.Title,
                    Author = model.Author,
                    Description = model.Description,
                    YearPublished = model.YearPublished,
                    DateRead = model.DateRead,
                    IsRecommended = model.IsRecommended,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Books.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<BookListItem> GetBooks()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Books
                        //.Where(e => e.OwnerID == _userID)
                        .Select(
                            e =>
                                new BookListItem
                                {
                                    BookID = e.BookID,
                                    Title = e.Title,
                                    Author = e.Author,
                                    Description = e.Description,
                                    YearPublished = e.YearPublished,
                                    DateRead = e.DateRead,
                                    IsRecommended = e.IsRecommended,
                                    CreatedUtc = e.CreatedUtc,
                                    ModifiedUtc = e.ModifiedUtc
                                }
                        );

                return query.ToArray();
            }
        }

        public BookDetail GetBookByID(int ID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Books
                        .Single(e => e.BookID == ID);// && e.OwnerID == _userID);
                return
                    new BookDetail
                    {
                        BookID = entity.BookID,
                        Title = entity.Title,
                        Author = entity.Author,
                        Description = entity.Description,
                        YearPublished = entity.YearPublished,
                        DateRead = entity.DateRead,
                        IsRecommended = entity.IsRecommended,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        public bool UpdateBook(BookEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Books
                        .Single(e => e.BookID == model.BookID);// && e.OwnerID == _userID);//This is Linq

                entity.BookID = model.BookID;
                entity.Title = model.Title;
                entity.Author = model.Author;
                entity.Description = model.Description;
                entity.YearPublished = model.YearPublished;
                entity.DateRead = model.DateRead;
                entity.IsRecommended = model.IsRecommended;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteBook(int bookID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Books
                        .Single(e => e.BookID == bookID);// && e.OwnerID == _userID);

                ctx.Books.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}


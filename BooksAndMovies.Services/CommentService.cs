using BooksAndMovies.Data;
using BooksAndMovies.Models.CommentModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BooksAndMovies.Services
{
    public class CommentService
    {
        private readonly Guid _userID;

        public CommentService(Guid userID)
        {
            _userID = userID;
        }

        public bool CreateComment(CommentCreate model)
        {
            var entity =
                new Comment()
                {
                    OwnerID = _userID,
                    BookID = model.BookID,
                    Book = model.Book,
                    //MovieID = model.MovieID,
                    //Movie = model.Movie,
             //       ShowID = model.ShowID,
               //     Show = model.Show,
                 //   TheaterProductionID = model.TheaterProductionID,
                   // TheaterProduction = model.TheaterProduction,
                    TypeOfMedia = model.TypeOfMedia,
                    MyComment = model.MyComment,
                    IsRecommended = model.IsRecommended,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Comments.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CommentListItem> GetComments()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Comments
                        .Where(e => e.OwnerID == _userID)
                        .Select(
                            e =>
                                new CommentListItem
                                {
                                    CommentID = e.CommentID,
                                    BookID = e.BookID,
                                    Book = e.Book,
                                   // MovieID = e.MovieID,
                                   // Movie = e.Movie,
                                   // ShowID = e.ShowID,
                                   // Show = e.Show,
                                   // TheaterProductionID = e.TheaterProductionID,
                                   // TheaterProduction = e.TheaterProduction,
                                    TypeOfMedia = e.TypeOfMedia,
                                    MyComment = e.MyComment,
                                    IsRecommended = e.IsRecommended,
                                    CreatedUtc = e.CreatedUtc,
                                    ModifiedUtc = e.ModifiedUtc
                                }
                        );

                return query.ToArray();
            }
        }

        public CommentDetail GetCommentByID(int ID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Comments
                        .Single(e => e.CommentID == ID);// && e.OwnerID == _userID);
                return
                    new CommentDetail
                    {
                        CommentID = entity.CommentID,
                        BookID = entity.BookID,
                        Book = entity.Book,
                       // MovieID = entity.MovieID,
                       // Movie = entity.Movie,
                       // ShowID = entity.ShowID,
                       // Show = entity.Show,
                       // TheaterProductionID = entity.TheaterProductionID,
                       // TheaterProduction = entity.TheaterProduction,
                        TypeOfMedia = entity.TypeOfMedia,
                        MyComment = entity.MyComment,
                        IsRecommended = entity.IsRecommended,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        public bool UpdateComment(CommentEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Comments
                        .Single(e => e.CommentID == model.CommentID);// && e.OwnerID == _userID);//This is Linq

                entity.CommentID = model.CommentID;
                //entity.BookID = model.BookID;
                //entity.Book = model.Book;
               // entity.MovieID = model.MovieID;
               // entity.Movie = model.Movie;
              //  entity.ShowID = model.ShowID;
               // entity.Show = model.Show;
               // entity.TheaterProductionID = model.TheaterProductionID;
              //  entity.TheaterProduction = model.TheaterProduction;
                entity.TypeOfMedia = model.TypeOfMedia;
                entity.MyComment = model.MyComment;
                entity.IsRecommended = model.IsRecommended;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteComment(int commentID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Comments
                        .Single(e => e.CommentID == commentID);// && e.OwnerID == _userID);

                ctx.Comments.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}

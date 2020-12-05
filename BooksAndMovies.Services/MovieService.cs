using BooksAndMovies.Data;
using BooksAndMovies.Models.MovieModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BooksAndMovies.Services
{
    public class MovieService
    {
        private readonly Guid _userID;

        public MovieService(Guid userID)
        {
            _userID = userID;
        }
        public MovieService()
        {

        }


        public bool CreateMovie(MovieCreate model)
        {
            var entity =
                new Movie()
                {
                    OwnerID = _userID,
                    Title = model.Title,
                    MainCast = model.MainCast,
                    Description = model.Description,
                    YearReleased = model.YearReleased,
                    DateWatched = model.DateWatched,
                    IsRecommended = model.IsRecommended,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Movies.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<MovieListItem> GetMovies()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Movies
                        //.Where(e => e.OwnerID == _userID)
                        .Select(
                            e =>
                                new MovieListItem
                                {
                                    MovieID = e.MovieID,
                                    Title = e.Title,
                                    MainCast = e.MainCast,
                                    Description = e.Description,
                                    YearReleased = e.YearReleased,
                                    DateWatched = e.DateWatched,
                                    IsRecommended = e.IsRecommended,
                                    CreatedUtc = e.CreatedUtc,
                                    ModifiedUtc = e.ModifiedUtc
                                }
                        );

                return query.ToArray();
            }
        }

        public MovieDetail GetMovieByID(int ID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Movies
                        .Single(e => e.MovieID == ID);// && e.OwnerID == _userID);
                return
                    new MovieDetail
                    {
                        MovieID = entity.MovieID,
                        Title = entity.Title,
                        MainCast = entity.MainCast,
                        Description = entity.Description,
                        YearReleased = entity.YearReleased,
                        DateWatched = entity.DateWatched,
                        IsRecommended = entity.IsRecommended,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        public bool UpdateMovie(MovieEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Movies
                        .Single(e => e.MovieID == model.MovieID);// && e.OwnerID == _userID);//This is Linq

                entity.Title = model.Title;
                entity.MainCast = model.MainCast;
                entity.Description = model.Description;
                entity.YearReleased = model.YearReleased;
                entity.DateWatched = model.DateWatched;
                entity.IsRecommended = model.IsRecommended;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteMovie(int movieID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Movies
                        .Single(e => e.MovieID == movieID);// && e.OwnerID == _userID);

                ctx.Movies.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}

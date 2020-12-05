using BooksAndMovies.Data;
using BooksAndMovies.Models.ShowModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.Services
{
    public class ShowService
    {
        private readonly Guid _userID;

        public ShowService(Guid userID)
        {
            _userID = userID;
        }
        public ShowService()
        {

        }


        public bool CreateShow(ShowCreate model)
        {
            var entity =
                new Show()
                {
                    OwnerID = _userID,
                    Title = model.Title,
                    MainCast = model.MainCast,
                    Description = model.Description,
                    YearReleased = model.YearReleased,
                    YearStarted = model.YearStarted,
                    YearFinished = model.YearFinished,
                    IsRecommended = model.IsRecommended,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Shows.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ShowListItem> GetShows()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Shows
                        //.Where(e => e.OwnerID == _userID)
                        .Select(
                            e =>
                                new ShowListItem
                                {
                                    ShowID = e.ShowID,
                                    Title = e.Title,
                                    MainCast = e.MainCast,
                                    Description = e.Description,
                                    YearReleased = e.YearReleased,
                                    YearStarted = e.YearStarted,
                                    YearFinished = e.YearFinished,
                                    IsRecommended = e.IsRecommended,
                                    CreatedUtc = e.CreatedUtc,
                                    ModifiedUtc = e.ModifiedUtc
                                }
                        );

                return query.ToArray();
            }
        }

        public ShowDetail GetShowByID(int ID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Shows
                        .Single(e => e.ShowID == ID);// && e.OwnerID == _userID);
                return
                    new ShowDetail
                    {
                        ShowID = entity.ShowID,
                        Title = entity.Title,
                        MainCast = entity.MainCast,
                        Description = entity.Description,
                        YearReleased = entity.YearReleased,
                        YearStarted = entity.YearStarted,
                        YearFinished = entity.YearFinished,
                        IsRecommended = entity.IsRecommended,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        public bool UpdateShow(ShowEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Shows
                        .Single(e => e.ShowID == model.ShowID);// && e.OwnerID == _userID);//This is Linq

                entity.ShowID = model.ShowID;
                entity.Title = model.Title;
                entity.MainCast = model.MainCast;
                entity.Description = model.Description;
                entity.YearReleased = model.YearReleased;
                entity.YearStarted = model.YearStarted;
                entity.YearFinished = model.YearFinished;
                entity.IsRecommended = model.IsRecommended;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteShow(int showID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Shows
                        .Single(e => e.ShowID == showID);// && e.OwnerID == _userID);

                ctx.Shows.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}

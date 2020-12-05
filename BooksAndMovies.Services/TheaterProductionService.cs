using BooksAndMovies.Data;
using BooksAndMovies.Models.TheaterProductionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.Services
{
    public class TheaterProductionService
    {
        private readonly Guid _userID;

        public TheaterProductionService(Guid userID)
        {
            _userID = userID;
        }
        public TheaterProductionService()
        {

        }


        public bool CreateTheaterProduction(TheaterProductionCreate model)
        {
            var entity =
                new TheaterProduction()
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
                ctx.TheaterProductions.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<TheaterProductionListItem> GetTheaterProductions()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .TheaterProductions
                        //.Where(e => e.OwnerID == _userID)
                        .Select(
                            e =>
                                new TheaterProductionListItem
                                {
                                    TheaterProductionID = e.TheaterProductionID,
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

        public TheaterProductionDetail GetTheaterProductionByID(int ID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .TheaterProductions
                        .Single(e => e.TheaterProductionID == ID);// && e.OwnerID == _userID);
                return
                    new TheaterProductionDetail
                    {
                        TheaterProductionID = entity.TheaterProductionID,
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

        public bool UpdateTheaterProduction(TheaterProductionEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .TheaterProductions
                        .Single(e => e.TheaterProductionID == model.TheaterProductionID);// && e.OwnerID == _userID);//This is Linq

                entity.TheaterProductionID = model.TheaterProductionID;
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

        public bool DeleteTheaterProduction(int theaterProductionID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .TheaterProductions
                        .Single(e => e.TheaterProductionID == theaterProductionID);// && e.OwnerID == _userID);

                ctx.TheaterProductions.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}

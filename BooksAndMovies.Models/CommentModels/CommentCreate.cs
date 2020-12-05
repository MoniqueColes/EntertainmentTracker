using BooksAndMovies.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.Models.CommentModels
{
    public class CommentCreate
    {
        public int CommentID { get; set; }
        [Display(Name = "Media Type")]
        public MediaType TypeOfMedia { get; set; }
        [Display(Name = "Comment")]
        public string MyComment { get; set; }
        [Display(Name = "Recommended")]
        public bool IsRecommended { get; set; }
        [Display(Name = "Comment Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTimeOffset CreatedUtc { get; set; }
        public int BookID { get; set; }
        //public int MovieID { get; set; }
        //public int ShowID { get; set; }
        //public int TheaterProductionID { get; set; }
        public virtual Book Book { get; set; }
        //public virtual Movie Movie { get; set; }
        //public virtual Show Show { get; set; }
        //public virtual TheaterProduction TheaterProduction { get; set; }
    }
}

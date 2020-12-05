using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.Models.TheaterProductionModels
{
    public class TheaterProductionCreate
    {
        [Required]
        public string Title { get; set; }
        [Display(Name = "Main Billing Cast")]
        public string MainCast { get; set; }
        public string Description { get; set; }
        [Display(Name = "Year Released")]
        public string YearReleased { get; set; }
        [Display(Name = "Date Watched")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime DateWatched { get; set; }
        [Display(Name = "Recommended")]
        [DefaultValue(false)]
        public bool IsRecommended { get; set; }
        [Display(Name = "Entry Created")]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}

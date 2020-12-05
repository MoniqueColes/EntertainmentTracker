using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.Models.ShowModels
{
    public class ShowDetail
    {
        [Key]
        public int ShowID { get; set; }
        [Required]
        public string Title { get; set; }
        [Display(Name = "Main Billing Cast")]
        public string MainCast { get; set; }
        public string Description { get; set; }
        [Display(Name = "Year Released")]
        public string YearReleased { get; set; }
        [Display(Name = "Year Started")]
        public string YearStarted { get; set; }
        [Display(Name = "Year Finished")]
        public string YearFinished { get; set; }
        [Display(Name = "Recommended")]
        [DefaultValue(false)]
        public bool IsRecommended { get; set; }
        [Display(Name = "Entry Created")]
        public DateTimeOffset CreatedUtc { get; set; }
        [Display(Name = "Entry Modified")]
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}

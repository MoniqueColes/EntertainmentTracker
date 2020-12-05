using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.Models.BookModels
{
    public class BookEdit
    {
        [Key]
        public int BookID { get; set; }
        [Required]
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        [Display(Name = "Year Published")]
        public string YearPublished { get; set; }
        [Display(Name = "Date Finished")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime DateRead { get; set; }
        [Display(Name = "Recommended")]
        [DefaultValue(false)]
        public bool IsRecommended { get; set; }
        [Display(Name = "Entry Modified")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}

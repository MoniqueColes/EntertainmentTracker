using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BooksAndMovies.Data
{
    public class Movie
    {
        [Key]
        public int MovieID { get; set; }
        [Required]
        public Guid UserID { get; set; }
        public string Title { get; set; }
        [Display(Name= "Main billing cast")]
        public string MainCast { get; set; }
        public string Description { get; set; }
        [Display(Name = "Year Released")]
        [MaxLength(12, ErrorMessage = "There are too many characters in this field.")]
        public int YearReleased { get; set; }
        public DateTime DateWatched { get; set; }
        [DefaultValue(false)]
        public bool IsRecommended { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BooksAndMovies.Data
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }
        [Required]
        public Guid OwnerID { get; set; }
        [Required]
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public int YearPublished { get; set; }
        [Display(Name = "Date Read")]
        public DateTime DatePicker { get; set; }
        [DefaultValue(false)]
        public bool IsRecommended { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}

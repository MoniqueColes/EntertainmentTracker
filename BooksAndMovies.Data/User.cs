using System;
using System.ComponentModel.DataAnnotations;

namespace BooksAndMovies.Data
{
    public class User
    {
        [Key]
        public Guid UserID { get; set; }
        [Required]
        [MaxLength(40, ErrorMessage = "There are too many characters in this field.")]
        public string Name { get; set; }
        [Display(Name = "Date Joined")]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}

using System;
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
        public string YearPublished { get; set; }
        public DateTime DateRead { get; set; }
        public bool IsRecommended { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
        //public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}

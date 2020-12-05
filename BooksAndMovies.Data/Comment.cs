using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BooksAndMovies.Data
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }
        [Required]
        public Guid UserID { get; set; }
        [Required]
        public int BookID { get; set; }
        [Required]
        public int MovieID { get; set; }
        [Required]
        public string MyComment { get; set; }
        [DefaultValue(false)]
        public bool IsRecommended { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksAndMovies.Data
{
    public enum MediaType
    {
        Book,
        //Movie,
        //Show,
        //TheaterProduction
    }
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }
        [Required]
        public Guid OwnerID { get; set; }
        public int BookID { get; set; }
        [ForeignKey(nameof(BookID))]
        public virtual Book Book { get; set; }
        //public int MovieID { get; set; }
        //[ForeignKey(nameof(MovieID))]
        //public virtual Movie Movie { get; set; }
        //public int ShowID { get; set; }
        //[ForeignKey(nameof(ShowID))]
        //public virtual Show Show { get; set; }
        //public int TheaterProductionID { get; set; }
        //[ForeignKey(nameof(TheaterProductionID))]
        //public virtual TheaterProduction TheaterProduction { get; set; }
        [Required]
        [Display(Name = "Media Type")]
        public MediaType TypeOfMedia { get; set; }
        [Required]
        [Display(Name ="Comment")]
        public string MyComment { get; set; }
        [DefaultValue(false)]
        public bool IsRecommended { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}

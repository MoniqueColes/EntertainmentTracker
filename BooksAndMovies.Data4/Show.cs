﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.Data
{
    public class Show
    {
        [Key]
        public int ShowID { get; set; }
        [Required]
        public Guid OwnerID { get; set; }
        [Required]
        public string Title { get; set; }
        public string MainCast { get; set; }
        public string Description { get; set; }
        public string YearReleased { get; set; }
        public string YearStarted { get; set; }
        public string YearFinished { get; set; }
        public bool IsRecommended { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
       // public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}

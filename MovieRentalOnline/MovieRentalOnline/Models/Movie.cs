using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieRentalOnline.Models
{
    public class Movie
    {
        public Movie()
        {
            this.Actors = new HashSet<Actor>();
            this.Directors = new HashSet<Director>();
            this.Genres = new HashSet<Genre>();
        }
        public int MovieID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate {get; set;}

        public virtual ICollection<Actor> Actors { get; set; }
        public virtual ICollection<Director> Directors { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
    }
}
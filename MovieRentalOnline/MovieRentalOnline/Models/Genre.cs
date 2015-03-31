using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieRentalOnline.Models
{
    public class Genre
    {
        public Genre() { }
        int GenreId { get; set; }
        string GenreName { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
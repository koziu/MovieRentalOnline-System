using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieRentalOnline.ViewModels
{
    public class FilterModel
    {
        public ICollection<GenreFilter> GenreFilter { get; set; }
        public ICollection<ActorFilter> ActorFilter { get; set; }
    }

    public class GenreFilter
    {
        public String Genre { get; set; }
    }
    public class ActorFilter
    {
        public string Name { get; set; }
    }
}
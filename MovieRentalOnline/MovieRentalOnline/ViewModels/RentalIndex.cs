using MovieRentalOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieRentalOnline.ViewModels
{
    public class RentalIndex
    {
        public List<List<Movie>> MoviesList;
        public List<List<Actor>> ActorsList;
    }
}
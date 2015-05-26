using MovieRentalOnline.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieRentalOnline.ViewModels
{
    public class MovieViewModel
    {
        public int MovieId { get; set; }   //klucz glowny
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string PhotoFileName { get; set; }
        public List<Actor> actorsList { get; set; }
        public List<Director> directorsList { get; set; }
        public List<Genre> genresList { get; set; } 
        
        
    }
}
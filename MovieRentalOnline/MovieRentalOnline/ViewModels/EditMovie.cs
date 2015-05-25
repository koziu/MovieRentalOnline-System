using MovieRentalOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieRentalOnline.ViewModels
{
    public class EditMovie
    {
        public Movie movie { get; set; }
        public IEnumerable<SelectListItem> AllActors { get; set; }
        public IEnumerable<SelectListItem> AllGenres { get; set; }
        public IEnumerable<SelectListItem> AllDirectors { get; set; }

        private List<int> _selectedActors;
        private List<int> _selectedGenress;
        private List<int> _selectedDirectors;

        public List<int> SelectedActors
        {
            get
            {
                if (_selectedActors == null)
                {
                    _selectedActors = movie.Actors.Select(m => m.ActorId).ToList();
                }
                return _selectedActors;
            }
            set { _selectedActors = value; }
        }

        public List<int> SelectedGenres
        {
            get
            {
                if (_selectedGenress == null)
                {
                    _selectedGenress = movie.Genres.Select(m => m.GenreId).ToList();
                }
                return _selectedGenress;
            }
            set { _selectedGenress = value; }
        }

        public List<int> SelectedDiretors
        {
            get
            {
                if (_selectedDirectors == null)
                {
                    _selectedDirectors = movie.Directors.Select(m => m.DirectorId).ToList();
                }
                return _selectedDirectors;
            }
            set { _selectedDirectors = value; }
        }

    }
}
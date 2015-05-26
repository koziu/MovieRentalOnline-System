using MovieRentalOnline.DAL;
using MovieRentalOnline.Models;
using MovieRentalOnline.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieRentalOnline.Controllers
{
    public class RentalController : Controller
    {

        private RentalContext db = new RentalContext();
        // GET: Rental
        public ActionResult Index()
        {
            var movies = db.Movies.ToList();
            var actors = db.Actors.ToList();

            List<Actor> a1 = actors.Take(5).ToList();
            List<Actor> a2 = actors.Where(a => !a1.Contains(a)).Take(5).ToList();


            List<Movie> m1 = movies.Take(4).ToList();
            List<Movie> m2 = movies.Where(a => !m1.Contains(a)).Take(4).ToList();



            RentalIndex viewModel = new RentalIndex();

            viewModel.ActorsList = new List<List<Actor>> { a1, a2 };
            viewModel.MoviesList = new List<List<Movie>> { m1, m2 };


            return View(viewModel);
        }

        public ActionResult List(FilterModel Filters = null)
        {
            if (Request.IsAjaxRequest())
            {
                var movies = db.Movies.ToList();
                if (Filters.ActorFilter != null)
                {
                    var actors = db.Actors.ToList();
                    foreach (var actor in Filters.ActorFilter)
                    {
                        var names = actor.Name.Split(' ');
                        foreach (var name in names)
                        {
                            actors = actors.Where(a => a.FirstName.ToLower().Contains(name.ToLower()) || a.LastName.ToLower().Contains(name.ToLower())).ToList();
                        }
                        movies = movies.Where(a => a.Actors.Any(b => actors.Contains(b))).ToList();
                    }
                }

                if (Filters.GenreFilter != null)
                {
                    movies = movies.Where(a => a.Genres.Any(b => Filters.GenreFilter.Any(c => c.Genre == b.GenreName))).ToList();
                }
                if (Filters.Title != null && Filters.Title != "")
                    movies = movies.Where(a => a.Title.ToLower().Contains(Filters.Title.ToLower())).ToList();
                return PartialView("_ResoultList", movies);
            }
            return View();
        }

        [ChildActionOnly]
        public ActionResult ResoultList()
        {

            var movies = db.Movies.ToList();
            return View("_ResoultList", movies);
        }


        [ChildActionOnly]
        public ActionResult GenreFilter()
        {
            var genres = db.Genres.ToList();
            return View("_GenreFilter", genres);
        }

        [ChildActionOnly]
        public ActionResult StorageMediumFilter()
        {
            var storageMediums = db.StorageMediums.ToList();
            return View("_StorageMediumFilter", storageMediums);
        }

        [ChildActionOnly]
        public ActionResult ActorFilter()
        {
            var actors = db.Actors.ToList();
            return View("_ActorFilter", actors);
        }

        public ActionResult ActorSuggestions(string term)
        {
            var actors = db.Actors.ToList();
            var names = term.Split(' ');
            foreach (var name in names)
            {

                actors = actors.Where(a => a.FirstName.ToLower().Contains(name.ToLower()) || a.LastName.ToLower().Contains(name.ToLower())).ToList();
            }

            var actor = actors.Take(5).Select(a => new { label = (a.FirstName + " " + a.LastName) });

            return Json(actor, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FiltrMovies(FilterModel Filters = null)
        {
            var actors = db.Actors.ToList();

            return Json(actors, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Movie(int id = -1)
        {
            var movie = db.Movies.Where(a => a.MovieId == id).FirstOrDefault();
            if (movie == null)
                return RedirectToAction("List");
            else
                return View(movie);
        }

        [ChildActionOnly]
        public ActionResult TitleFilter()
        {
            return View("_TitleFilter");
        }

        public ActionResult TitleSuggestions(string term)
        {
            var movies = db.Movies.Where(a => a.Title.ToLower().Contains(term.ToLower())).ToList();
            var movie = movies.Take(5).Select(a => new { label = (a.Title) });

            return Json(movie, JsonRequestBehavior.AllowGet);
        }

        
    }
}
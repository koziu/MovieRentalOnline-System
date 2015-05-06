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

            viewModel.ActorsList = new List<List<Actor>>{a1,a2};
            viewModel.MoviesList = new List<List<Movie>>{m1,m2};


            return View(viewModel);
        }

        public ActionResult List()
        {
            var movies = db.Movies.ToList();
            return View(movies);
        }

        public ActionResult Detail(int id)
        {
            return View();
        }

    }
}
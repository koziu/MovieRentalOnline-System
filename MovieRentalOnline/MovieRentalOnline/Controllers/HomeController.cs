using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieRentalOnline.CustomAtributes;
using MovieRentalOnline.DAL;
using MovieRentalOnline.Models;

namespace MovieRentalOnline.Controllers
{
    public class HomeController : Controller
    {
        private RentalContext db = new RentalContext();
        [AdjustLayout]
        public ActionResult Index()
        {
            // dodawanie n

            // pobieranie obiektow z bazy


            return View(db.Movies.ToList());
        }
        [AdjustLayout]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [AdjustLayout]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AdminPanel()
        {
            return View();
        }
    }
}
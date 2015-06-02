using System.Linq;
using System.Web.Mvc;
using MovieRentalOnline.CustomAtributes;
using MovieRentalOnline.DAL;

namespace MovieRentalOnline.Controllers
{
      
    public class HomeController : Controller
    {
        private RentalContext db = new RentalContext();
        
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
        [AdjustLayout]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminPanel()
        {
            return View();
        }
    }
}
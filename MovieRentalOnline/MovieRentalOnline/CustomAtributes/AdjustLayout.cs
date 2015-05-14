using System.Web;
using System.Web.Mvc;

namespace MovieRentalOnline.CustomAtributes
{
    public class AdjustLayout : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            var result = filterContext.Result as ViewResult;
            if (result != null)
            {
                if (HttpContext.Current.User.IsInRole("Admin"))
                {
                    result.MasterName = "~/Views/Shared/_AdminLayout.cshtml";
                }
                else
                {
                    result.MasterName = HttpContext.Current.User.IsInRole("Worker") ? "~/Views/Shared/_AdminLayout.cshtml" : "~/Views/Shared/_Layout.cshtml";
           
                }
                 }
        }
    }
}
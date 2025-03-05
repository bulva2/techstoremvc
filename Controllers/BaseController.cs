using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TechStoreMVC.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.IsAuthenticated = context.HttpContext.Session.GetString("username") != null;
            ViewBag.Username = context.HttpContext.Session.GetString("username");
            ViewBag.Role = context.HttpContext.Session.GetString("role");
            base.OnActionExecuting(context);
        }
    }
}

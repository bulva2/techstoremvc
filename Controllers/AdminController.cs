using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TechStoreMVC.Controllers
{
    public class AdminController : SecuredController
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("role") != "admin")
            {
                context.Result = new RedirectToActionResult("Index", "Home", null);
            }

            base.OnActionExecuting(context);
        }
    }
}

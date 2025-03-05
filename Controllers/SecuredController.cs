using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TechStoreMVC.Controllers
{
    public class SecuredController : BaseController
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("username") == null)
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
            }

            base.OnActionExecuting(context);
        }
    }
}

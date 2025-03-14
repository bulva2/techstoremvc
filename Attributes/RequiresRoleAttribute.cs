using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TechStoreMVC.Attributes
{
    public class RequiresRoleAttribute : ActionFilterAttribute
    {
        private string? _role;

        public RequiresRoleAttribute(string role)
        {
            _role = role;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _role = context.HttpContext.Session.GetString("role");

            if (_role == null || _role != "admin")
            {
                context.Result = new RedirectToActionResult("Index", "Home", null);
            }

            base.OnActionExecuting(context);
        }
    }
}

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;

namespace InventorySystem.Models
{
    public class CheckSession: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string check = filterContext.HttpContext.Session.GetString("UserID");
            if (filterContext.HttpContext.Session.GetString("UserID") == "0" || filterContext.HttpContext.Session.GetString("UserID") == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                     new RouteValueDictionary {
                                { "Controller", "Login" },
                                { "Action", "Login" }
                                 });
            }
        }
    }
}

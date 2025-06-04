using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class RequireLogin : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var hasCookie = context.HttpContext.Request.Cookies.ContainsKey("LoggedInUser");
        if (!hasCookie)
        {
            context.Result = new RedirectToActionResult("EmployeeLoginPage", "Authentication", null);
        }
    }
}
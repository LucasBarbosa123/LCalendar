using LCalendar.Dtos;
using LCalendar.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class RequireLogin : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var cookies = context.HttpContext.Request.Cookies;
        if (!cookies.ContainsKey("LoggedInUser"))
        {
            context.Result = new RedirectToActionResult("EmployeeLoginPage", "Authentication", null);
            return;
        }

        // retrieve cookie data
        var cookieValue = cookies["LoggedInUser"];
        var decoded = System.Net.WebUtility.UrlDecode(cookieValue);
        decoded = decoded.Replace(" ", "+");    //basically the way the coockie is serialize makes we do this
        var user = System.Text.Json.JsonSerializer.Deserialize<EmployeeLoginCoockie>(decoded);

        // if the cookie is to hold we destroy it and trit it has invalid
        if (user == null || !user.IsStillValid())
        {
            context.HttpContext.Response.Cookies.Delete("LoggedInUser");
            context.Result = new RedirectToActionResult("EmployeeLoginPage", "Authentication", null);
            return;
        }
    }
}
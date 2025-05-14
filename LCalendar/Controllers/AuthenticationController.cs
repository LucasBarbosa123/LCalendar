using Microsoft.AspNetCore.Mvc;

namespace LCalendar.Controllers;

public class AuthenticationController : Controller
{
    // GET
    public IActionResult EmployeeLoginPage()
    {
        return View();
    }
}
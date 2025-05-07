using Microsoft.AspNetCore.Mvc;

namespace LCalendar.Controllers;

public class RolesController : Controller
{
    public IActionResult RolesPage()
    {
        return View();
    }
}
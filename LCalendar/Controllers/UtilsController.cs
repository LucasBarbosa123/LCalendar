using LCalendar.Utils;
using Microsoft.AspNetCore.Mvc;

namespace LCalendar.Controllers;

public class UtilsController : Controller
{
    [HttpGet]
    public IActionResult GetGeneratedPassword()
    {
        return Ok(GeneralUtils.GeneratePassword());
    }
}
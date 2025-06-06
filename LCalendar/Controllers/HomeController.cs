using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LCalendar.Models;

namespace LCalendar.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _dbContext;

    public HomeController(ILogger<HomeController> logger,  AppDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [RequireLogin]
    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
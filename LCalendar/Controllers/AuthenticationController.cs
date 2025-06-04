using System.Text.Json;
using LCalendar.Dtos;
using LCalendar.Utils;
using Microsoft.AspNetCore.Mvc;

namespace LCalendar.Controllers;

public class AuthenticationController (AppDbContext dbContext) : Controller
{
    // GET
    public IActionResult EmployeeLoginPage()
    {
        return View();
    }

    [HttpPost]
    public IActionResult TryLoginFromEmployeePage([FromBody] LoginFromEmployeePageInfo loginFromEmployeePageInfo)
    {
        if ((loginFromEmployeePageInfo.Password ?? "") == "" || (loginFromEmployeePageInfo.Email ?? "") == "")
        {
            return Unauthorized(new { errorMsg = "Bowth email and password need to be filled" });
        }
        
        var employee = dbContext.Employees.Where(e => e.Email == loginFromEmployeePageInfo.Email).FirstOrDefault();
        if (employee == null)
        {
            return Unauthorized("Email or password incorrect.");
        }
        
        var authenticationSuccess = GeneralUtils.VerifyPassword(loginFromEmployeePageInfo.Password, (employee.Password ?? ""));
        if (authenticationSuccess == false)
        {
            return Unauthorized("Email or password incorrect.");
        }

        var expiringDate = DateTimeOffset.UtcNow.AddMinutes(30);
        if (loginFromEmployeePageInfo.Remember == true)
        {
            expiringDate = DateTimeOffset.UtcNow.AddYears(30);
        }
        
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = expiringDate
        };

        var newCoockie = new EmployeeLoginCoockie(employee.Id);
        HttpContext.Response.Cookies.Append("LoggedInUser", JsonSerializer.Serialize(newCoockie), cookieOptions);

        
        
        return Json(new { redirectUrl = "/" });
    }
}
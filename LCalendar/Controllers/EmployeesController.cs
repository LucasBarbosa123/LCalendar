using LCalendar.Dtos;
using LCalendar.Models;
using LCalendar.Utils;
using Microsoft.AspNetCore.Mvc;

namespace LCalendar.Controllers;

public class EmployeesController (AppDbContext dbContext) : Controller
{
    public IActionResult EmployeesPage()
    {
        var employees = dbContext.Employees.ToList();
        var fullEmployeesInfo = employees.ConvertToFullInfo(dbContext);
        ViewBag.Employees = fullEmployeesInfo;
        
        var roles = dbContext.Roles.ToList();
        ViewBag.Roles = roles;
        
        return View();
    }

    [HttpPost]
    public IActionResult CreateEmployee([FromBody] EmployeeInfo employeeInfo)
    {
        var newEmployee = new Employee
        {
            Name = employeeInfo.Name,
            Email = employeeInfo.Email,
            Password = GeneralUtils.HashPassword(employeeInfo.Password),
            CreationTime = DateTime.Now,
            RoleId = employeeInfo.RoleId,
            Img = employeeInfo.Img
        };
        dbContext.Employees.Add(newEmployee);
        dbContext.SaveChanges();
        
        return Created();
    }

    [HttpPut]
    public IActionResult UpdateEmployee([FromQuery] int id, [FromBody] EmployeeInfo employeeInfo)
    {
        var employee = dbContext.Employees.Where(e => e.Id == id).FirstOrDefault();
        if (employee == null)
        {
            return BadRequest("No employee was found from this Id.");
        }
        
        employee.Name = employeeInfo.Name;
        employee.Email = employeeInfo.Email;
        employee.Password = GeneralUtils.HashPassword(employeeInfo.Password);
        employee.CreationTime = DateTime.Now;
        employee.RoleId = employeeInfo.RoleId;
        employee.Img = employeeInfo.Img;
        dbContext.SaveChanges();
        
        return Ok();
    }

    [HttpDelete]
    public IActionResult DeleteEmployee([FromQuery] int id)
    {
        var employee = dbContext.Employees.Where(e => e.Id == id).FirstOrDefault();
        if (employee == null)
        {
            return BadRequest("No employee was found from this Id.");
        }
        
        dbContext.Employees.Remove(employee);
        dbContext.SaveChanges();
        
        return Ok();
    }
    
    [HttpGet]
    public IActionResult GetEmployeeInfo([FromQuery] int id)
    {
        var employee = dbContext.Employees.Where(e => e.Id == id).FirstOrDefault();
        if (employee == null)
        {
            return BadRequest("No employee was found from this Id.");
        }

        var employeeInfo = employee.ConvertToEmployeeInfo();
        
        return Ok(employeeInfo);
    }
}
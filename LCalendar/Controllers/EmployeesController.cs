using LCalendar.Dtos;
using LCalendar.Models;
using LCalendar.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LCalendar.Controllers;

public class EmployeesController (AppDbContext dbContext) : Controller
{
    [RequireLogin]
    public IActionResult EmployeesPage()
    {
        var roles = dbContext.Roles.ToList();
        ViewBag.Roles = roles;
        
        return View();
    }

    [RequireLogin]
    [HttpPost]
    public IActionResult CreateEmployee([FromBody] EmployeeInfo employeeInfo)
    {
        var newEmployee = employeeInfo.ConvertToEmployee();
        dbContext.Employees.Add(newEmployee);
        dbContext.SaveChanges();
        
        return Created();
    }

    [RequireLogin]
    [HttpPut]
    public IActionResult UpdateEmployee([FromQuery] int id, [FromBody] EmployeeInfo employeeInfo)
    {
        var employee = dbContext.Employees.Where(e => e.Id == id).FirstOrDefault();
        if (employee == null)
        {
            return BadRequest("Employee not found");
        }
        
        //the other properties will be changeable in other endpoints
        employee.Name = employeeInfo.Name;
        employee.Email = employeeInfo.Email;
        employee.RoleId = employeeInfo.RoleId;
        dbContext.SaveChanges();
        
        return Ok();
    }

    [RequireLogin]
    [HttpDelete]
    public IActionResult DeleteEmployee([FromQuery] int id)
    {
        var employee = dbContext.Employees.Where(e => e.Id == id).FirstOrDefault();
        if (employee == null)
        {
            return BadRequest("Employee not found");
        }
        
        employee.IsDeleted = true;
        dbContext.SaveChanges();
        
        return Ok();
    }
    
    [RequireLogin]
    [HttpGet]
    public IActionResult GetEmployeeInfo([FromQuery] int id)
    {
        var employee = dbContext.Employees.Where(e => e.Id == id).FirstOrDefault();
        if (employee == null)
        {
            return BadRequest("Employee not found");
        }

        var employeeInfo = employee.ConvertToEmployeeInfo();
        
        return Ok(employeeInfo);
    }

    [RequireLogin]
    public IActionResult GetAllEmployeesInfos()
    {
        var allEmployees = dbContext.Employees.Where(e => !e.IsDeleted).ToList()
            .Select(e => new
            {
                name = e.Name,
                email = e.Email,
                role = RolesUtils.GetRoleNameById((e.RoleId ?? 0), dbContext),
                creationTime = e.CreationTime,
                buttons = GeneralUtils.GetTableEditDeleteButtons(e.Id, "startEmployeeDelition", "startEmployeeEdition")
            }).ToList();
        
        return Ok(allEmployees);
    }
}
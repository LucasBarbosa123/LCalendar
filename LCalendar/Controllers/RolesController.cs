using LCalendar.Dtos;
using LCalendar.Utils;
using Microsoft.AspNetCore.Mvc;

namespace LCalendar.Controllers;

public class RolesController (AppDbContext dbContext) : Controller
{
    public IActionResult RolesPage()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CreateRole([FromBody] RoleInfo roleInfo)
    {
        var newRole = roleInfo.ConvertToRole();
        dbContext.Roles.Add(newRole);
        dbContext.SaveChanges();
        
        return Created();
    }
    
    [HttpPut]
    public IActionResult UpdateRole([FromQuery] int id, [FromBody] RoleInfo roleInfo)
    {
        var role = dbContext.Roles.Where(r => r.Id == id).FirstOrDefault();
        if (role == null)
        {
            return BadRequest("Role not found");
        }
        
        role.Name = roleInfo.Name;
        dbContext.SaveChanges();
        
        return Ok();
    }
    
    [HttpDelete]
    public IActionResult DeleteRole([FromQuery] int id)
    {
        var role = dbContext.Roles.Where(r => r.Id == id).FirstOrDefault();
        if (role == null)
        {
            return BadRequest("Role not found");
        }
        
        dbContext.Roles.Remove(role);
        dbContext.SaveChanges();
        
        return Ok();
    }
    
    [HttpGet]
    public IActionResult GetRoleInfo([FromQuery] int id)
    {
        var role = dbContext.Roles.Where(r => r.Id == id).FirstOrDefault();
        if (role == null)
        {
            return BadRequest("Role not found");
        }

        var roleInfo = role.ConvertToRoleInfo();
        
        return Ok(roleInfo);
    }
    
    [HttpGet]
    public IActionResult GetAllRolesInfos()
    {
        var allRoles = dbContext.Roles.Select(r => new
        {
            number = r.Id,
            name = r.Name,
            buttons = GeneralUtils.GetTableEditDeleteButtons(r.Id, "startRoleDelition", "startRoleEdition")
        }).ToList();
        
        return Ok(allRoles);
    }
}
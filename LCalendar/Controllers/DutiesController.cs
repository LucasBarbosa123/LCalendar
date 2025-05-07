using LCalendar.Dtos;
using LCalendar.Utils;
using Microsoft.AspNetCore.Mvc;

namespace LCalendar.Controllers;

public class DutiesController (AppDbContext dbContext) : Controller
{
    // GET
    public IActionResult DutiesPage()
    {
        var duties = dbContext.Duties.ToList();
        ViewBag.Duties = duties;
        return View();
    }

    [HttpPost]
    public IActionResult CreateDutie([FromBody] DutieInfo dutieInfo)
    {
        var newDutie = dutieInfo.ConvertToDutie();
        dbContext.Duties.Add(newDutie);
        dbContext.SaveChanges();
        
        return Created();
    }

    [HttpPut]
    public IActionResult UpdateDutie([FromQuery] int id, [FromBody] DutieInfo dutieInfo)
    {
        var dutie = dbContext.Duties.Where(d => d.Id == id).FirstOrDefault();
        if (dutie == null)
        {
            return BadRequest("Dutie not found");
        }
        
        dutie.Name = dutieInfo.Name;
        dutie.DefaultTime = TimeSpan.Parse(dutieInfo.DefaultTimeStr);
        dbContext.SaveChanges();
        
        return Ok();
    }

    [HttpDelete]
    public IActionResult DeleteDutie([FromQuery] int id)
    {
        var dutie = dbContext.Duties.Where(d => d.Id == id).FirstOrDefault();
        if (dutie == null)
        {
            return BadRequest("Dutie not found");
        }
        
        var employeeDuties = dbContext.EmployeeDuties.Where(e => e.DutyId == id).ToList();
        dbContext.EmployeeDuties.RemoveRange(employeeDuties);
        
        dbContext.Duties.Remove(dutie);
        dbContext.SaveChanges();
        
        return Ok();
    }

    [HttpGet]
    public IActionResult GetDutieInfo([FromQuery] int id)
    {   
        var dutie = dbContext.Duties.Where(d => d.Id == id).FirstOrDefault();
        if (dutie == null)
        {
            return BadRequest("Dutie not found");
        }
        
        var dutieInfo = dutie.ConvertToDutieInfo();
        return Ok(dutieInfo);
    }

    [HttpGet]
    public IActionResult GetAllDutiesInfos()
    {
        var duties = dbContext.Duties.Select(d => new
        {
            name = d.Name,
            defaultTime = (d.DefaultTime ?? TimeSpan.Zero).ToString(@"hh\:mm"),
            creationTime = d.CreationTime,
            buttons = GeneralUtils.GetTableEditDeleteButtons(d.Id, "startDutieDelition", "startDutieEdition")
        }).ToList();

        return Ok(duties);
    }
}
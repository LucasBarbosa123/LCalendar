using LCalendar.Dtos;
using LCalendar.Models;
using LCalendar.Utils;
using Microsoft.AspNetCore.Mvc;

namespace LCalendar.Controllers;

public class ClientsController (AppDbContext dbContext) : Controller
{
    public IActionResult ClientsPage()
    {
        var clients = dbContext.Clients.ToList();
        ViewBag.Clients = clients;
        
        return View();
    }

    [HttpDelete]
    public IActionResult DeleteClient(int id)
    {
        var client = dbContext.Clients.Where(c => c.Id == id).FirstOrDefault();
        if (client == null)
        {
            return BadRequest("No client was found from this Id.");
        }
        
        dbContext.Clients.Remove(client);
        dbContext.SaveChanges();

        return Ok();
    }

    [HttpPost]
    public IActionResult CreateClient([FromBody] ClientInfo clientInfo)
    {
        var newClient = new Client
        {
            Name = clientInfo.Name
        };
        dbContext.Clients.Add(newClient);
        dbContext.SaveChanges();
        
        return Created();
    }

    [HttpPut]
    public IActionResult UpdateClient([FromQuery] int id, [FromBody] ClientInfo clientInfo)
    {
        var client = dbContext.Clients.Where(c =>  c.Id == id).FirstOrDefault();
        if (client == null)
        {
            return BadRequest("No client was found from this Id.");
        }
        
        client.Name = clientInfo.Name;
        dbContext.SaveChanges();
        
        return Ok();
    }

    [HttpGet]
    public IActionResult GetClientInfo([FromQuery] int id)
    {
        var client = dbContext.Clients.Where(c => c.Id == id).FirstOrDefault();
        if (client == null)
        {
            return BadRequest("No client was found from this Id.");
        }
        
        var clientInfo = client.ConvertToClientInfo();
        return Ok(clientInfo);
    }
}
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using LCalendar.Dtos;
using Microsoft.AspNetCore.Mvc;
using LCalendar.Models;
using LCalendar.Utils;

namespace LCalendar.Controllers;

public class ApointmentsController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _dbContext;

    public ApointmentsController(ILogger<HomeController> logger,  AppDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    
    public IActionResult CalendarPage()
    {
        return View();
    }
    
    public IActionResult GetCalendarEvents([FromQuery] DateTime start, [FromQuery] DateTime end)
    {
        var allEvents = _dbContext.Apointments.ToList();
        
        //start and end are the first and last days that are appearing in the calendar
        var calendarEvents = _dbContext.Apointments.ToList()
            .Where(a => ApointmentsUtils.ApointmentToShow(start, end, a.ScheduledStart, a.ScheduledEnd))
            .Select(a => new CalendarEvent(a.Title, a.ScheduledStart, a.ScheduledEnd, 
                                                    new CalendarEventExtendedProps(a.Id, a.Description))
            )
            .ToList();
        return Json(calendarEvents);
    }

    [HttpPost]
    public IActionResult CreateAppointment([FromBody] CreateApointmentInfo appointmentInfos)
    {
        var parsedDate = DateOnly.Parse(appointmentInfos.Date);
        var parsedStart = TimeOnly.Parse(appointmentInfos.StartTime);
        var parsedEnd = TimeOnly.Parse(appointmentInfos.EndTime);

        var dateTimeStart = parsedDate.ToDateTime(parsedStart);
        var dateTimeEnd = parsedDate.ToDateTime(parsedEnd);

        var newAppointment = new Apointment
        {
            ClientId = 0,
            EmployeeId = 0,
            Title = appointmentInfos.Title,
            Description = appointmentInfos.Description,
            ScheduledStart = dateTimeStart,
            ScheduledEnd = dateTimeEnd,
            DurationMin = (int)Math.Ceiling((dateTimeEnd - dateTimeStart).TotalMinutes)
        };
        _dbContext.Apointments.Add(newAppointment);
        _dbContext.SaveChanges();
        
        return Ok();
    }
}
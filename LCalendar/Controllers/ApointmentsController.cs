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
    public IActionResult CreateAppointment([FromBody] ApointmentInfo appointmentInfos)
    {
        ApointmentsUtils.CalculateAppointmentDates(appointmentInfos.Date, appointmentInfos.StartTime, appointmentInfos.EndTime,
                                                    out DateTime start, out DateTime end);

        var newAppointment = new Apointment
        {
            ClientId = 0,
            EmployeeId = 0,
            Title = appointmentInfos.Title,
            Description = appointmentInfos.Description,
            ScheduledStart = start,
            ScheduledEnd = end,
            DurationMin = (int)Math.Ceiling((end - start).TotalMinutes)
        };
        _dbContext.Apointments.Add(newAppointment);
        _dbContext.SaveChanges();
        
        return Created();
    }

    [HttpPut]
    public IActionResult UpdateAppointment([FromQuery] int id, [FromBody] ApointmentInfo appointmentInfos)
    {
        var appointment = _dbContext.Apointments.Where(a => a.Id == id).FirstOrDefault();
        if (appointment == null)
        {
            return BadRequest("No appointment found from that Id.");
        }
        
        ApointmentsUtils.CalculateAppointmentDates(appointmentInfos.Date, appointmentInfos.StartTime, appointmentInfos.EndTime,
                                                    out DateTime start, out DateTime end);
        
        appointment.Title = appointmentInfos.Title;
        appointment.Description = appointmentInfos.Description;
        appointment.ScheduledStart = start;
        appointment.ScheduledEnd = end;
        appointment.DurationMin = (int)Math.Ceiling((end - start).TotalMinutes);
        _dbContext.SaveChanges();
        
        return Ok();
    }

    [HttpDelete]
    public IActionResult DeleteAppointment([FromQuery] int id)
    {
        var appointment = _dbContext.Apointments.Where(a => a.Id == id).FirstOrDefault();
        _dbContext.Apointments.Remove(appointment);
        _dbContext.SaveChanges();
        
        return Ok();
    }

    public IActionResult GetAppointment([FromQuery] int id)
    {
        var appointment = _dbContext.Apointments.Where(a => a.Id == id).FirstOrDefault();
        if (appointment == null)
        {
            return BadRequest("No appointment found from that Id.");
        }

        var appointmentInfo = appointment.ConvertToAppointmentInfo();
        return Ok(appointmentInfo);
    }
}
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using LCalendar.Dtos;
using Microsoft.AspNetCore.Mvc;
using LCalendar.Models;
using LCalendar.Utils;

namespace LCalendar.Controllers;

public class ApointmentsController (AppDbContext dbContext) : Controller
{
    public IActionResult CalendarPage()
    {
        var employees =  dbContext.Employees.ToList();
        ViewBag.Employees = employees;
        
        var clients = dbContext.Clients.ToList();
        ViewBag.Clients = clients;
        
        return View();
    }
    
    public IActionResult GetCalendarEvents([FromQuery] DateTime start, [FromQuery] DateTime end)
    {
        var allEvents = dbContext.Apointments.ToList();
        
        //start and end are the first and last days that are appearing in the calendar
        var calendarEvents = dbContext.Apointments.ToList()
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
            ClientId = appointmentInfos.ClientId,
            EmployeeId = appointmentInfos.EmployeeId,
            Title = appointmentInfos.Title,
            Description = appointmentInfos.Description,
            ScheduledStart = start,
            ScheduledEnd = end,
            DurationMin = (int)Math.Ceiling((end - start).TotalMinutes)
        };
        dbContext.Apointments.Add(newAppointment);
        dbContext.SaveChanges();
        
        return Created();
    }

    [HttpPut]
    public IActionResult UpdateAppointment([FromQuery] int id, [FromBody] ApointmentInfo appointmentInfos)
    {
        var appointment = dbContext.Apointments.Where(a => a.Id == id).FirstOrDefault();
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
        appointment.ClientId = appointmentInfos.ClientId;
        appointment.EmployeeId = appointmentInfos.EmployeeId;
        dbContext.SaveChanges();
        
        return Ok();
    }

    [HttpDelete]
    public IActionResult DeleteAppointment([FromQuery] int id)
    {
        var appointment = dbContext.Apointments.Where(a => a.Id == id).FirstOrDefault();
        if (appointment == null)
        {
            return BadRequest("No appointment found from that Id.");
        }
        
        dbContext.Apointments.Remove(appointment);
        dbContext.SaveChanges();
        
        return Ok();
    }

    public IActionResult GetAppointment([FromQuery] int id)
    {
        var appointment = dbContext.Apointments.Where(a => a.Id == id).FirstOrDefault();
        if (appointment == null)
        {
            return BadRequest("No appointment found from that Id.");
        }

        var appointmentInfo = appointment.ConvertToAppointmentInfo();
        return Ok(appointmentInfo);
    }
}
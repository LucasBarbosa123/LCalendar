using LCalendar.Dtos;
using LCalendar.Models;

namespace LCalendar.Utils;

public static class ApointmentsUtils
{
    public static bool ApointmentToShow(DateTime calendarStart, DateTime calendarEnd, DateTime? apointmentStart, DateTime? apointmentEnd)
    {
        if (apointmentStart == null || apointmentEnd == null)
        {
            return false;
        }

        var validStart = apointmentStart > calendarStart;
        var validEnd = apointmentEnd< calendarEnd.AddDays(1);
        
        return (validStart && validEnd);
    }

    public static void CalculateAppointmentDates(string date, string start, string end, out DateTime dateTimeStart, out DateTime dateTimeEnd)
    {
        var parsedDate = DateOnly.Parse(date);
        var parsedStart = TimeOnly.Parse(start);
        var parsedEnd = TimeOnly.Parse(end);

        dateTimeStart = parsedDate.ToDateTime(parsedStart);
        dateTimeEnd = parsedDate.ToDateTime(parsedEnd);
    }

    public static ApointmentInfo ConvertToAppointmentInfo(this Apointment appointment)
    {
        var appointmentInfo = new ApointmentInfo
        {
            Title = appointment.Title,
            Description = appointment.Description,
            Date = $"{appointment.ScheduledStart?.Year}-{appointment.ScheduledStart?.Month:D2}-{appointment.ScheduledStart?.Day:D2}",
            StartTime = $"{appointment.ScheduledStart?.Hour:D2}:{appointment.ScheduledStart?.Minute:D2}",
            EndTime = $"{appointment.ScheduledEnd?.Hour:D2}:{appointment.ScheduledEnd?.Minute:D2}"
        };

        return appointmentInfo;
    }
}
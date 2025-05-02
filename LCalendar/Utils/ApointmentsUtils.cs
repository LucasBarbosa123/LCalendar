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
}
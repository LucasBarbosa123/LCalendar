namespace LCalendar.Dtos;

public class CalendarEvent
{
    public string title { get; set; }
    public DateTime start { get; set; }
    public DateTime end { get; set; }
    public CalendarEventExtendedProps extendedProps { get; set; }

    public CalendarEvent(string? title, DateTime? start, DateTime? end, int id)
    {
        this.title = title ?? "";
        this.start = start ??  DateTime.MinValue;
        this.end = end ??  DateTime.MinValue;
        this.extendedProps = new CalendarEventExtendedProps(id);
    }
    public CalendarEvent(string? title, DateTime? start, DateTime? end, CalendarEventExtendedProps extendedProps)
    {
        this.title = title ?? "";
        this.start = start ??  DateTime.MinValue;
        this.end = end ??  DateTime.MinValue;
        this.extendedProps = extendedProps;
    }
}
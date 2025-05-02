namespace LCalendar.Dtos;

public class CalendarEventExtendedProps
{
    public int id { get; set; }
    public string description { get; set; }

    public CalendarEventExtendedProps(int id)
    {
        this.id = id;
        this.description = string.Empty;
    }
    
    public CalendarEventExtendedProps(int id, string description)
    {
        this.id = id;
        this.description = description;
    }
}
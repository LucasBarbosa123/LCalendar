namespace LCalendar.Models;

public class Apointment
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? ScheduledStart { get; set; }
    public DateTime? ScheduledEnd { get; set; }
    public int? DurationMin { get; set; }
    
    public int? EmployeeId { get; set; }
    public int? ClientId  { get; set; }
}
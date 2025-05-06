namespace LCalendar.Models;

public class EmployeeDutie
{
    public int Id { get; set; }
    
    public int EmployeeId { get; set; }
    public int DutyId { get; set; }
    public TimeSpan EstimatedTime { get; set; }
}
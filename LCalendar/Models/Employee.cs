namespace LCalendar.Models;

public class Employee
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Img { get; set; }
    public string? Password { get; set; }
    public DateTime? CreationTime { get; set; }
    public bool IsDeleted { get; set; } = false;
    
    public int? RoleId { get; set; }
}
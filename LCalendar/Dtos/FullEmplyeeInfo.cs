using LCalendar.Models;

namespace LCalendar.Dtos;

public class FullEmplyeeInfo
{
    public Employee? Employee { get; set; }
    public Role? Role { get; set; }

    public FullEmplyeeInfo()
    {
        this.Employee = new Employee();
        this.Role = new Role();
    }
    public FullEmplyeeInfo(Employee employee)
    {
        this.Employee = employee;
        this.Role = new Role();   
    }
}
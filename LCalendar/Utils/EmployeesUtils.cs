using LCalendar.Dtos;
using LCalendar.Models;

namespace LCalendar.Utils;

public static class EmployeesUtils
{
    public static List<FullEmplyeeInfo> ConvertToFullInfo(this List<Employee> employees, AppDbContext dbContext)
    {
        var fullInfoEmplyees = new List<FullEmplyeeInfo>();
        foreach (var employee in employees)
        {
            var fullInfoEmplyee = new FullEmplyeeInfo(employee);
            fullInfoEmplyee.Role = dbContext.Roles.Where(r => r.Id == employee.RoleId).FirstOrDefault();
            
            fullInfoEmplyees.Add(fullInfoEmplyee);
        }
        
        return fullInfoEmplyees;
    }

    public static EmployeeInfo ConvertToEmployeeInfo(this Employee employee)
    {
        var employeeInfo = new EmployeeInfo
        {
            Name = employee.Name,
            Email = employee.Email,
            RoleId = employee.RoleId,
            Img = employee.Img,
            
            Password = "",
            PasswordConfirmation = ""
        };
        
        return employeeInfo;
    }
}
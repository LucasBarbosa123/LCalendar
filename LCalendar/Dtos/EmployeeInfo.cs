using System.Text.Json.Serialization;
using LCalendar.Models;
using LCalendar.Utils;

namespace LCalendar.Dtos;

public class EmployeeInfo
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    [JsonPropertyName("email")]
    public string? Email { get; set; }
    
    [JsonPropertyName("img")]
    public string? Img { get; set; } = "";
    
    [JsonPropertyName("password")]
    public string? Password { get; set; }
    
    [JsonPropertyName("roleId")]
    public int? RoleId { get; set; }

    public Employee ConvertToEmployee()
    {
        var employee = new Employee
        {
            Name = this.Name,
            Email = this.Email,
            Password = GeneralUtils.HashPassword(this.Password),
            CreationTime = DateTime.Now,
            RoleId = this.RoleId,
            Img = this.Img
        };

        return employee;
    }
}
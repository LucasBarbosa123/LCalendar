using System.Text.Json.Serialization;
using LCalendar.Models;

namespace LCalendar.Dtos;

public class RoleInfo
{
    [JsonPropertyName("name")]
    public string Name { get; set; }


    public Role ConvertToRole()
    {
        var role = new Role();
        role.Name = this.Name;
        
        return role;
    }
}
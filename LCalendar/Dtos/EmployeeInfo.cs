using System.Text.Json.Serialization;

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
    
    [JsonPropertyName("passwordConfirmation")]
    public string? PasswordConfirmation { get; set; }
    
    [JsonPropertyName("roleId")]
    public int? RoleId { get; set; }
}
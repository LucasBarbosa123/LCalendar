using System.Text.Json.Serialization;

namespace LCalendar.Dtos;

public class EmployeeLoginCoockie
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }
    [JsonPropertyName("loginDate")]
    public DateTime? LoginDate { get; set; }
    [JsonPropertyName("keepMeLoggedIn")]
    public bool? KeepMeLoggedIn { get; set; }

    public EmployeeLoginCoockie() { } // 👈 needed for deserialization
    public EmployeeLoginCoockie(int id, bool keepMeLoggedIn = false)
    {
        this.Id = id;
        this.LoginDate = DateTime.Now;
        this.KeepMeLoggedIn = keepMeLoggedIn;
    }
}
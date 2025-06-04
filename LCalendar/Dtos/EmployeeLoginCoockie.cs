using System.Text.Json.Serialization;

namespace LCalendar.Dtos;

public class EmployeeLoginCoockie
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }
    [JsonPropertyName("loginDate")]
    public DateTime? LoginDate { get; set; }

    public EmployeeLoginCoockie(int id)
    {
        this.Id = id;
        this.LoginDate = DateTime.Now;
    }
}
using System.Text.Json.Serialization;

namespace LCalendar.Dtos;

public class CreateApointmentInfo
{
    [JsonPropertyName("title")]
    public string Title { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("date")]
    public string Date { get; set; }
    [JsonPropertyName("startTime")]
    public string StartTime { get; set; }
    [JsonPropertyName("endTime")]
    public string EndTime { get; set; }
}
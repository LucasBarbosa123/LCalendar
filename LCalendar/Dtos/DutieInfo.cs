using System.Text.Json.Serialization;
using LCalendar.Models;

namespace LCalendar.Dtos;

public class DutieInfo
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("defaultTime")]
    public string DefaultTimeStr { get; set; }

    public Dutie ConvertToDutie()
    {
        var dutie = new Dutie
        {
            Name = this.Name,
            DefaultTime = TimeSpan.Parse(this.DefaultTimeStr),
            CreationTime = DateTime.Now
        };
        
        return dutie;
    }
}
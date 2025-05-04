using System.Text.Json.Serialization;

namespace LCalendar.Dtos;

public class ClientInfo
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    public ClientInfo(string name)
    {
        this.Name = name;
    }
}
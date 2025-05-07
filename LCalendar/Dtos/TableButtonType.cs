namespace LCalendar.Dtos;

public class TableButtonType
{
    public string Type { get; set; }
    public string Classes { get; set; }
    
    public TableButtonType(string type, string classes)
    {
        this.Type = type;
        this.Classes = classes;
    }
}
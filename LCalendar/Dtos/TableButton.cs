namespace LCalendar.Dtos;

public class TableButton
{
    public string FunctionId { get; set; }
    public string FunctionName { get; set; }
    public string Icon { get; set; }
    public string ButtonType
    {
        get => this._ButtonType;
        set
        {
            this._ButtonType = value;
            this.TableButtonType = ButtonTypes.Where(bt => bt.Type == this.ButtonType).FirstOrDefault();
            if (TableButtonType == null)
            {
                throw new Exception("There is no ButtonType with that type name, please create it or use another.");
            }
        }
    }

    private string _ButtonType { get; set; }
    private TableButtonType? TableButtonType { get; set; }

    public TableButton(string FunctionId, string FunctionName, string Icon, string ButtonType)
    {
        this.FunctionId = FunctionId;
        this.FunctionName = FunctionName;
        this.Icon = Icon;
        this.ButtonType = ButtonType;
    }

    public static List<TableButtonType> ButtonTypes { get; } = new List<TableButtonType>
    {
        new TableButtonType("PRIMARY", "btn bg-primary-subtle rounded-circle round-40 d-inline-flex align-items-center justify-content-center"),
        new TableButtonType("SECONDARY", "btn bg-secondary-subtle rounded-circle round-40 d-inline-flex align-items-center justify-content-center"),
        new TableButtonType("DANGER", "btn bg-danger-subtle text-danger rounded-circle round-40 d-inline-flex align-items-center justify-content-center"),
        new TableButtonType("WARNING", "btn bg-warning-subtle rounded-circle round-40 d-inline-flex align-items-center justify-content-center"),
    };

    public string ToString(string additionalClasses = "")
    {
        var classes = $"{TableButtonType.Classes} {additionalClasses}";
        var str = $"<button type=\"button\" class=\"{classes}\" onclick=\"{this.FunctionName}('{this.FunctionId}')\"> {this.Icon} </button>";
        return str;
    }
}
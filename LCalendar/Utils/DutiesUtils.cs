using LCalendar.Dtos;
using LCalendar.Models;

namespace LCalendar.Utils;

public static class DutiesUtils
{
    public static DutieInfo ConvertToDutieInfo(this Dutie dutie)
    {
        var dutieInfo = new DutieInfo
        {
            Name = dutie.Name ?? "",
            DefaultTimeStr = dutie.DefaultTime?.ToString(@"hh\:mm") ?? "00:00",
        };
        return dutieInfo;
    }
}
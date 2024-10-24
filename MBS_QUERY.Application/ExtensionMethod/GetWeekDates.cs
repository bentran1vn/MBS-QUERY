namespace MBS_QUERY.Application.ExtensionMethod;
public static class GetWeekDates
{
    public static List<DateOnly> Get(DateOnly dateOnly)
    {
        var dates = new List<DateOnly>();
        var dayOfWeek = (int)dateOnly.DayOfWeek;
        var startOfWeek = dateOnly.AddDays(-dayOfWeek);
        for (var i = 0; i < 7; i++)
        {
            dates.Add(startOfWeek.AddDays(i));
        }

        return dates;
    }
}
namespace Core.Utility;

public class DateTimeRange
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }

    public DateTimeRange(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }

    public bool IsDateIncludedInRange(DateTime currentDate)
    {
        return currentDate >= Start && currentDate <= End;
    }
}

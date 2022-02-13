using System.Collections;

namespace Core.Tests.Data;

public class DateTimeData : IEnumerable<object[]>
{
    private readonly IEnumerable<object[]> _children = new List<object[]>
        {
            new object[] { DateTime.UtcNow, DateTime.UtcNow, "0 secs" },
            new object[] { new DateTime(2022, 2, 13, 19, 37, 30), new DateTime(2022, 2, 13, 19, 37, 28), "2 secs" },
            new object[] { new DateTime(2022, 2, 13, 19, 37, 30), new DateTime(2022, 2, 13, 19, 34, 28), "3 mins" },
            new object[] { new DateTime(2022, 2, 13, 19, 37, 30), new DateTime(2022, 2, 13, 19, 37, 28), "2 secs" }
        };

    public IEnumerator<object[]> GetEnumerator() => _children.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

using Core.Tests.Data;
using Core.Utility;
using Xunit;

namespace Core.Tests.UnitTests;

public class DateTimeExtensionsTests
{
    [ClassData(typeof(DateTimeData))]
    [Theory]
    public void Date_To_Message_Time_Hint_Test(
        DateTime dateTime1,
        DateTime dateTime2,
        string result)
    {
        var s = dateTime1.Minutes(dateTime2);

        Assert.Equal(result, s);
    }
}

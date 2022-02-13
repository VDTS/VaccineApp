using Core.Utility;
using System.Globalization;

namespace Core.Converters;
public class DateToMessageTimeHint : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var age = (DateTime)value;
        return age.Minutes();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? 1 : 0;
    }

   
}
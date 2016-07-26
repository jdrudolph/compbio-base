using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace BaseLib.Wpf
{
    public class StringToRegexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Regex) value).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new Regex((string) value);
        }
    }
}
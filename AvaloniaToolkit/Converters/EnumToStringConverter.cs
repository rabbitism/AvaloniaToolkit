using System;
using System.Globalization;
using System.Windows.Data;
using System.Linq;
using System.ComponentModel.DataAnnotations;

#nullable enable

namespace AvaloniaToolkit.Converters
{
    internal class EnumToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                Type type = value.GetType();
                var member = type.GetMember(value.ToString());
                var attributes = member[0].GetCustomAttributes(typeof(DisplayAttribute), true);
                var attribute = attributes[0] as DisplayAttribute;
                var result= attribute?.Name ?? value.ToString();
                return result;
            }
            catch
            {
                return value.ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

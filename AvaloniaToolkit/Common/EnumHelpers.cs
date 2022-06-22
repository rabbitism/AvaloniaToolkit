using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaToolkit.Common
{
    public static class EnumHelpers
    {
        public static string GetDisplayName<T>(T value)
        {
            Type type = value.GetType();
            var member = type.GetMember(value.ToString());
            var attributes = member[0].GetCustomAttributes(typeof(DisplayAttribute), true);
            var attribute = attributes[0] as DisplayAttribute;
            var result = attribute?.Name ?? value.ToString();
            return result;
        }

        public static List<T> GetEnumValues<T>() where T: struct, System.Enum
        {
            return typeof(T).GetEnumValues().Cast<T>().ToList();
        }
    }
}

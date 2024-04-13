using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ThinkSharp.Licensing.Helper
{
    internal static class DateTimeHelper
    {
        public static DateTime NoExpiryDateTime { get; } = DateTime.MaxValue.SerializeDateTime().DeserializeDateTime();
        public static string SerializeDateTime(this DateTime d) => d.ToString(CultureInfo.InvariantCulture);
        public static DateTime DeserializeDateTime(this string dateTimeString) => DateTime.Parse(dateTimeString, CultureInfo.InvariantCulture);
    }
}

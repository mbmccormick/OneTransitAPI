using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneTransitAPI.Transit.Common
{
    public static class Utilities
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static readonly TimeZoneInfo EST = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
        
        public static double ConvertToRadians(double val)
        {
            return val * (Math.PI / 180);
        }

        public static double DifferenceInRadians(double val1, double val2)
        {
            return ConvertToRadians(val2) - ConvertToRadians(val1);
        }

        public static double Distance(double lat1, double lng1, double lat2, double lng2)
        {
            double radius = 6367000.0;
            return radius * 2 * Math.Asin(Math.Min(1, Math.Sqrt((Math.Pow(Math.Sin((DifferenceInRadians(lat1, lat2)) / 2.0), 2.0) + Math.Cos(ConvertToRadians(lat1)) * Math.Cos(ConvertToRadians(lat2)) * Math.Pow(Math.Sin((DifferenceInRadians(lng1, lng2)) / 2.0), 2.0)))));
        }

        public static DateTime ConvertFromUnixTime(int timeZoneOffset, string text)
        {
            double seconds = double.Parse(text, System.Globalization.CultureInfo.InvariantCulture) / 1000;
            return Epoch.AddSeconds(seconds).AddHours(-timeZoneOffset);
        }
    }
}
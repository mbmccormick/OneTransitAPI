﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OneTransitAPI.Data;

namespace OneTransitAPI.Common
{
    public class Utilities
    {
        public static void LogEvent(string category, string message)
        {
            DatabaseDataContext db = new DatabaseDataContext();

            Information e = new Information();
            e.Message = "[" + category + "] " + message;
            e.CreatedDate = DateTime.UtcNow;

            db.Informations.InsertOnSubmit(e);

            db.SubmitChanges();
        }

        public static TimeZoneInfo OlsonTimeZoneToTimeZoneInfo(string olsonTimeZoneId)
        {
            var olsonWindowsTimes = new Dictionary<string, string>()
            {
                { "Africa/Cairo", "Egypt Standard Time" },
                { "Africa/Casablanca", "Morocco Standard Time" },
                { "Africa/Johannesburg", "South Africa Standard Time" },
                { "Africa/Lagos", "W. Central Africa Standard Time" },
                { "Africa/Nairobi", "E. Africa Standard Time" },
                { "Africa/Windhoek", "Namibia Standard Time" },
                { "America/Anchorage", "Alaskan Standard Time" },
                { "America/Asuncion", "Paraguay Standard Time" },
                { "America/Bogota", "SA Pacific Standard Time" },
                { "America/Buenos_Aires", "Argentina Standard Time" },
                { "America/Caracas", "Venezuela Standard Time" },
                { "America/Cayenne", "SA Eastern Standard Time" },
                { "America/Chicago", "Central Standard Time" },
                { "America/Chihuahua", "Mountain Standard Time (Mexico)" },
                { "America/Cuiaba", "Central Brazilian Standard Time" },
                { "America/Denver", "Mountain Standard Time" },
                { "America/Edmonton", "Mountain Standard Time" },
                { "America/Godthab", "Greenland Standard Time" },
                { "America/Guatemala", "Central America Standard Time" },
                { "America/Halifax", "Atlantic Standard Time" },
                { "America/Indianapolis", "US Eastern Standard Time" },
                { "America/Indiana/Indianapolis", "US Eastern Standard Time" },
                { "America/La_Paz", "SA Western Standard Time" },
                { "America/Los_Angeles", "Pacific Standard Time" },
                { "America/Mexico_City", "Mexico Standard Time" },
                { "America/Montevideo", "Montevideo Standard Time" },
                { "America/New_York", "Eastern Standard Time" },
                { "America/Phoenix", "US Mountain Standard Time" },
                { "America/Regina", "Canada Central Standard Time" },
                { "America/Santa_Isabel", "Pacific Standard Time (Mexico)" },
                { "America/Santiago", "Pacific SA Standard Time" },
                { "America/Sao_Paulo", "E. South America Standard Time" },
                { "America/St_Johns", "Newfoundland Standard Time" },
                { "Asia/Almaty", "Central Asia Standard Time" },
                { "Asia/Amman", "Jordan Standard Time" },
                { "Asia/Baghdad", "Arabic Standard Time" },
                { "Asia/Baku", "Azerbaijan Standard Time" },
                { "Asia/Bangkok", "SE Asia Standard Time" },
                { "Asia/Beirut", "Middle East Standard Time" },
                { "Asia/Calcutta", "India Standard Time" },
                { "Asia/Colombo", "Sri Lanka Standard Time" },
                { "Asia/Damascus", "Syria Standard Time" },
                { "Asia/Dhaka", "Bangladesh Standard Time" },
                { "Asia/Dubai", "Arabian Standard Time" },
                { "Asia/Irkutsk", "North Asia East Standard Time" },
                { "Asia/Jerusalem", "Israel Standard Time" },
                { "Asia/Kabul", "Afghanistan Standard Time" },
                { "Asia/Kamchatka", "Kamchatka Standard Time" },
                { "Asia/Karachi", "Pakistan Standard Time" },
                { "Asia/Katmandu", "Nepal Standard Time" },
                { "Asia/Krasnoyarsk", "North Asia Standard Time" },
                { "Asia/Magadan", "Magadan Standard Time" },
                { "Asia/Novosibirsk", "N. Central Asia Standard Time" },
                { "Asia/Rangoon", "Myanmar Standard Time" },
                { "Asia/Riyadh", "Arab Standard Time" },
                { "Asia/Seoul", "Korea Standard Time" },
                { "Asia/Shanghai", "China Standard Time" },
                { "Asia/Singapore", "Singapore Standard Time" },
                { "Asia/Taipei", "Taipei Standard Time" },
                { "Asia/Tashkent", "West Asia Standard Time" },
                { "Asia/Tbilisi", "Georgian Standard Time" },
                { "Asia/Tehran", "Iran Standard Time" },
                { "Asia/Tokyo", "Tokyo Standard Time" },
                { "Asia/Ulaanbaatar", "Ulaanbaatar Standard Time" },
                { "Asia/Vladivostok", "Vladivostok Standard Time" },
                { "Asia/Yakutsk", "Yakutsk Standard Time" },
                { "Asia/Yekaterinburg", "Ekaterinburg Standard Time" },
                { "Asia/Yerevan", "Armenian Standard Time" },
                { "Atlantic/Azores", "Azores Standard Time" },
                { "Atlantic/Cape_Verde", "Cape Verde Standard Time" },
                { "Atlantic/Reykjavik", "Greenwich Standard Time" },
                { "Australia/Adelaide", "Cen. Australia Standard Time" },
                { "Australia/Brisbane", "E. Australia Standard Time" },
                { "Australia/Darwin", "AUS Central Standard Time" },
                { "Australia/Hobart", "Tasmania Standard Time" },
                { "Australia/Perth", "W. Australia Standard Time" },
                { "Australia/Sydney", "AUS Eastern Standard Time" },
                { "Etc/GMT", "UTC" },
                { "Etc/GMT+11", "UTC-11" },
                { "Etc/GMT+12", "Dateline Standard Time" },
                { "Etc/GMT+2", "UTC-02" },
                { "Etc/GMT-12", "UTC+12" },
                { "Europe/Berlin", "W. Europe Standard Time" },
                { "Europe/Budapest", "Central Europe Standard Time" },
                { "Europe/Istanbul", "GTB Standard Time" },
                { "Europe/Kiev", "FLE Standard Time" },
                { "Europe/London", "GMT Standard Time" },
                { "Europe/Minsk", "E. Europe Standard Time" },
                { "Europe/Moscow", "Russian Standard Time" },
                { "Europe/Paris", "Romance Standard Time" },
                { "Europe/Warsaw", "Central European Standard Time" },
                { "Indian/Mauritius", "Mauritius Standard Time" },
                { "Pacific/Apia", "Samoa Standard Time" },
                { "Pacific/Auckland", "New Zealand Standard Time" },
                { "Pacific/Fiji", "Fiji Standard Time" },
                { "Pacific/Guadalcanal", "Central Pacific Standard Time" },
                { "Pacific/Honolulu", "Hawaiian Standard Time" },
                { "Pacific/Port_Moresby", "West Pacific Standard Time" },
                { "Pacific/Tongatapu", "Tonga Standard Time" }
            };

            var windowsTimeZoneId = default(string);
            var windowsTimeZone = default(TimeZoneInfo);
            if (olsonWindowsTimes.TryGetValue(olsonTimeZoneId, out windowsTimeZoneId))
            {
                try { windowsTimeZone = TimeZoneInfo.FindSystemTimeZoneById(windowsTimeZoneId); }
                catch (TimeZoneNotFoundException) { }
                catch (InvalidTimeZoneException) { }
            }
            return windowsTimeZone;
        }
    }
}

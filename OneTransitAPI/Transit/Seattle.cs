using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Configuration;
using OneTransitAPI.Data;
using OneTransitAPI.Transit.Common;

namespace OneTransitAPI.Transit
{
    public class Seattle : IWebService
    {
        public Seattle(Agency transitAgency) : base(transitAgency)
        {
            this.APIKey = ConfigurationManager.AppSettings["SeattleKey"];
        }

        public override List<Stop> GetStopsByLocation(double latitude, double longitude, double radius)
        {
            System.Net.WebClient client = new System.Net.WebClient();
            var jsonResult = client.DownloadString("http://api.onebusaway.org/api/where/stops-for-location.json?key=" + APIKey + "&agency=" + this.TransitAgency.AgencyID + "&lat=" + latitude + "&lon=" + longitude + "&radius=" + radius);

            List<Stop> result = new List<Stop>();

            foreach (var r in Json.Decode(jsonResult).data.stops)
            {
                Stop s = new Stop();
                s.ID = r.id;
                s.Name = r.name;
                s.Code = r.code;
                s.Latitude = Convert.ToDouble(r.lat);
                s.Longitude = Convert.ToDouble(r.lon);

                result.Add(s);
            }

            return result;
        }

        public override List<StopTime> GetStopTimes(string stopID)
        {
            System.Net.WebClient client = new System.Net.WebClient();
            var jsonResult = client.DownloadString("http://api.onebusaway.org/api/where/arrivals-and-departures-for-stop/" + stopID + ".json?key=" + APIKey + "&version=2");

            List<StopTime> result = new List<StopTime>();

            var response = Json.Decode(jsonResult).data;

            foreach (var r in response.entry.arrivalsAndDepartures)
            {
                StopTime t = new StopTime();
                t.RouteShortName = r.routeShortName;
                t.RouteLongName = r.routeLongName;
                if (r.predicted.ToString().ToLower() == "true")
                {
                    t.ArrivalTime = Utilities.ConvertFromUnixTime(this.TransitAgency.TimeZone, r.predictedArrivalTime.ToString()).TimeOfDay;
                    t.DepartureTime = Utilities.ConvertFromUnixTime(this.TransitAgency.TimeZone, r.predictedDepartureTime.ToString()).TimeOfDay;
                    t.Type = 1;
                }
                else
                {
                    t.ArrivalTime = Utilities.ConvertFromUnixTime(this.TransitAgency.TimeZone, r.scheduledArrivalTime.ToString()).TimeOfDay;
                    t.DepartureTime = Utilities.ConvertFromUnixTime(this.TransitAgency.TimeZone, r.scheduledDepartureTime.ToString()).TimeOfDay;
                    t.Type = 0;
                }

                if (Convert.ToBoolean(ConfigurationManager.AppSettings["IsDaylightSavingsTime"]) == true)
                {
                    t.ArrivalTime = t.ArrivalTime.Add(new TimeSpan(1, 0, 0));
                    t.DepartureTime = t.DepartureTime.Add(new TimeSpan(1, 0, 0));
                }
                         
                if ((from x in result where x.RouteShortName == t.RouteShortName select x).Count() < 2)
                    result.Add(t);
            }

            return result;
        }
    }
}
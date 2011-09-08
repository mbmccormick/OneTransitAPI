using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Configuration;
using OneTransitAPI.Data;
using OneTransitAPI.Transit.Common;

namespace OneTransitAPI.Transit.Common
{
    public class RouteShout : IWebService 
    {
        public RouteShout(Agency transitAgency) : base(transitAgency)
        {
            this.APIKey = "97027712b04709de2958f77edcc25f1d";
        }

        public override List<Stop> GetStopsByLocation(double latitude, double longitude, double radius)
        {
            System.Net.WebClient client = new System.Net.WebClient();
            var jsonResult = client.DownloadString("http://api.routeshout.com/v1/rs.stops.getListByLocation?key=" + APIKey + "&agency=" + this.TransitAgency.AgencyID + "&lat=" + latitude + "&lon=" + longitude + "&limit=1");
            
            List<Stop> result = new List<Stop>();

            foreach (var r in Json.Decode(jsonResult).response)
            {
                if (Utilities.Distance(latitude, longitude, Convert.ToDouble(r.lat), Convert.ToDouble(r.lon)) <= radius)
                {
                    Stop s = new Stop();
                    s.ID = r.id;
                    s.Name = r.name;
                    s.Code = r.code;
                    s.Latitude = Convert.ToDouble(r.lat);
                    s.Longitude = Convert.ToDouble(r.lon);

                    result.Add(s);
                }
            }

            return result;
        }

        public override List<StopTime> GetStopTimes(string stopID)
        {
            System.Net.WebClient client = new System.Net.WebClient();
            var jsonResult = client.DownloadString("http://api.routeshout.com/v1/rs.stops.getTimes?key=" + APIKey + "&agency=" + this.TransitAgency.AgencyID + "&stop=" + stopID);

            List<StopTime> result = new List<StopTime>();

            foreach (var r in Json.Decode(jsonResult).response)
            {
                StopTime t = new StopTime();
                t.RouteShortName = r.route_short_name;
                t.RouteLongName = r.route_kong_name;
                t.ArrivalTime = Convert.ToDateTime(r.arrival_time.ToString()).TimeOfDay;
                t.DepartureTime = Convert.ToDateTime(r.departure_time.ToString()).TimeOfDay;
                t.Type = Convert.ToInt32(r.type);

                if ((from x in result where x.RouteShortName == t.RouteShortName select x).Count() < 2)
                    result.Add(t);
            }

            return result;
        }
    }
}
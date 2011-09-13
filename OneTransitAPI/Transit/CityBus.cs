using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using OneTransitAPI.Data;
using OneTransitAPI.Transit.Common;

namespace OneTransitAPI.Transit
{
    public class CityBus : IWebService
    {
        public CityBus(Agency transitAgency) : base(transitAgency)
        {
        }

        public override List<Route> GetRoutes()
        {
            GTFS engine = new GTFS(this.TransitAgency);
            List<Route> result = engine.GetRoutes();

            return result;
        }

        public override Stop GetStop(string stopid)
        {
            GTFS engine = new GTFS(this.TransitAgency);
            Stop result = engine.GetStop(stopid);

            return result;
        }

        public override List<Stop> GetStops()
        {
            GTFS engine = new GTFS(this.TransitAgency);
            List<Stop> result = engine.GetStops();

            return result;
        }

        public override List<Stop> GetStopsByLocation(double latitude, double longitude, double radius)
        {
            GTFS engine = new GTFS(this.TransitAgency);
            List<Stop> result = engine.GetStopsByLocation(latitude, longitude, radius);

            return result;
        }

        public override List<StopTime> GetStopTimes(string stopid)
        {
            System.Net.WebClient client = new System.Net.WebClient();
            
            GTFS engine = new GTFS(this.TransitAgency);
            Stop stop = engine.GetStop(stopid);

            DataSet ds = new DataSet();
            ds.ReadXml(client.OpenRead("http://myride.gocitybus.com/widget/Default1.aspx?pt=30&code=" + stop.Code));

            List<StopTime> result = new List<StopTime>();

            if (ds.Tables["Bus"] == null)
            {
                return result;
            }

            foreach (DataRow r in ds.Tables["Bus"].Rows)
            {
                StopTime t = new StopTime();
                
                t.RouteShortName = r["RouteName"].ToString().Substring(0, r["RouteName"].ToString().IndexOf(" ")).ToUpper().Trim(); if (t.RouteShortName.Length > 3) t.RouteShortName = t.RouteShortName.Substring(0, 3);
                t.RouteLongName = r["RouteName"].ToString().Replace(t.RouteShortName, "").Trim();

                var utc = new DateTimeOffset(DateTime.UtcNow, TimeSpan.Zero);
                var now = utc.ToOffset(this.TransitAgency.FriendlyTimeZone.GetUtcOffset(utc)).ToLocalTime();
                                
                if (r["TimeTillArrival"].ToString() == "DUE")
                    t.ArrivalTime = now.DateTime.ToString("t");
                else
                    t.ArrivalTime = now.AddMinutes(Convert.ToInt32(r["TimeTillArrival"].ToString().Replace("min", "").Trim())).DateTime.ToString("t");
                
                t.DepartureTime = t.ArrivalTime;
                t.Type = 1;

                result.Add(t);
            }

            return result;
        }
    }
}
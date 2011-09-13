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
    public class WMATA : IWebService
    {
        public WMATA(Agency transitAgency) : base(transitAgency)
        {
            this.APIKey = "sr3rfhh9347x4tvgsvaaxakn";
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
            System.Net.WebClient client = new System.Net.WebClient();
            var jsonResult = client.DownloadString("http://api.wmata.com/Rail.svc/json/JStations?api_key=" + APIKey);

            List<Stop> result = new List<Stop>();

            var data = Json.Decode(jsonResult);

            if (data != null)
            {
                foreach (var r in data.Stations)
                {
                    if (Utilities.Distance(latitude, longitude, Convert.ToDouble(r.Lat), Convert.ToDouble(r.Lon)) <= radius)
                    {
                        Stop s = new Stop();
                        s.ID = r.Code;
                        s.Name = r.Name;
                        s.Code = r.Code;
                        s.Latitude = Convert.ToDouble(r.Lat);
                        s.Longitude = Convert.ToDouble(r.Lon);

                        result.Add(s);
                    }
                }
            }

            jsonResult = client.DownloadString("http://api.wmata.com/Bus.svc/json/JStops?api_key=" + APIKey + "&lat=" + latitude + "&lon=" + longitude + "&radius=" + radius);
            
            data = Json.Decode(jsonResult);
            
            if (data != null)
            {
                foreach (var r in data.Stops)
                {
                    Stop s = new Stop();
                    s.ID = r.StopID;
                    s.Name = r.Name;
                    s.Code = r.StopID;
                    s.Latitude = Convert.ToDouble(r.Lat);
                    s.Longitude = Convert.ToDouble(r.Lon);

                    result.Add(s);
                }
            }

            return result;
        }

        public override List<StopTime> GetStopTimes(string stopID)
        {
            System.Net.WebClient client = new System.Net.WebClient();
            var jsonResult = client.DownloadString("http://api.wmata.com/StationPrediction.svc/json/GetPrediction/" + stopID + "?api_key=" + APIKey);

            List<StopTime> result = new List<StopTime>();

            var data = Json.Decode(jsonResult);

            if (data != null)
            {
                foreach (var r in data.Trains)
                {
                    if (r.Min != "")
                    {
                        StopTime t = new StopTime();

                        t.RouteShortName = r.Line;
                        t.RouteLongName = r.Line;

                        var utc = new DateTimeOffset(DateTime.UtcNow, TimeSpan.Zero);
                        var now = utc.ToOffset(this.TransitAgency.FriendlyTimeZone.GetUtcOffset(utc)).ToLocalTime();

                        if (r.Min.ToString() == "ARR" ||
                            r.Min.ToString() == "BRD")
                            t.ArrivalTime = now.DateTime.ToString("t");
                        else
                            t.ArrivalTime = now.AddMinutes(Convert.ToInt32(r.Min.ToString())).DateTime.ToString("t");
                        
                        t.DepartureTime = t.ArrivalTime;
                        t.Type = 1;

                        if ((from x in result where x.RouteShortName == t.RouteShortName select x).Count() < 2)
                            result.Add(t);
                    }
                }
            }

            jsonResult = client.DownloadString("http://api.wmata.com/Bus.svc/json/JStopSchedule?stopId=" + stopID + "&api_key=" + APIKey);

            data = Json.Decode(jsonResult);

            if (data != null)
            {
                foreach (var r in data.ScheduleArrivals)
                {
                    StopTime t = new StopTime();
                    t.RouteShortName = r.RouteID;
                    t.RouteLongName = r.RouteID;
                    t.ArrivalTime = DateTime.ParseExact(r.ScheduleTime.ToString(), "s", new System.Globalization.CultureInfo("en-US")).TimeOfDay;
                    t.DepartureTime = t.ArrivalTime;
                    t.Type = 0;

                    if ((from x in result where x.RouteShortName == t.RouteShortName select x).Count() < 2)
                        result.Add(t);
                }
            }

            return result;
        }
    }
}
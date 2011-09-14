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
    public class BART : IWebService
    {
        public BART(Agency transitAgency) : base(transitAgency)
        {
            this.APIKey = "ETDH-BAEA-Y9UQ-9TXU";
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

        public override List<StopTime> GetStopTimes(string stopID)
        {
            System.Net.WebClient client = new System.Net.WebClient();

            DataSet ds = new DataSet();
            ds.ReadXml(client.OpenRead("http://api.bart.gov/api/etd.aspx?cmd=etd&key=" + APIKey + "&orig=" + stopID));

            List<StopTime> result = new List<StopTime>();

            foreach (DataRow r in ds.Tables["etd"].Rows)
            {
                foreach (DataRow s in ds.Tables["estimate"].Rows)
                {
                    if (s["etd_Id"].ToString() == r["etd_Id"].ToString())
                    {
                        StopTime t = new StopTime();

                        t.RouteShortName = r["abbreviation"].ToString();
                        t.RouteLongName = r["destination"].ToString();

                        var utc = new DateTimeOffset(DateTime.UtcNow, TimeSpan.Zero);
                        var now = utc.ToOffset(this.TransitAgency.FriendlyTimeZone.GetUtcOffset(utc));

                        if (s["minutes"].ToString() == "Arrived")
                            t.ArrivalTime = now.DateTime.ToString("t");
                        else
                            t.ArrivalTime = now.AddMinutes(Convert.ToInt32(s["minutes"].ToString().Trim())).DateTime.ToString("t");
                        
                        t.DepartureTime = t.ArrivalTime;
                        t.Type = 1;

                        if ((from x in result where x.RouteShortName == t.RouteShortName select x).Count() < 2)
                            result.Add(t);
                    }
                }
            }

            return result;
        }
    }
}
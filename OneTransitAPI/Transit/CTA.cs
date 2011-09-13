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
    public class CTA : IWebService
    {
        public CTA(Agency transitAgency) : base(transitAgency)
        {
            this.APIKey = "jGijvDCQLFimphXidZxyJ8UPL";
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
            ds.ReadXml(client.OpenRead("http://www.ctabustracker.com/bustime/api/v1/getpredictions?key=" + APIKey + "&stpid=" + stopID));

            List<StopTime> result = new List<StopTime>();

            if (ds.Tables["prd"] == null)
            {
                return result;
            }

            foreach (DataRow r in ds.Tables["prd"].Rows)
            {
                StopTime t = new StopTime();
                t.RouteShortName = r["rt"].ToString();
                t.RouteLongName = r["rt"].ToString();
                t.ArrivalTime = DateTime.ParseExact(r["prdtm"].ToString(), "yyyyMMdd HH:mm", new System.Globalization.CultureInfo("en-US")).ToString("t");
                t.DepartureTime = t.ArrivalTime;
                t.Type = 1;

                if ((from x in result where x.RouteShortName == t.RouteShortName select x).Count() < 2)
                    result.Add(t);
            }

            return result;
        }
    }
}
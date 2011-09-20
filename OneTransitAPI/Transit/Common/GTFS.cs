using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using System.Configuration;
using Microsoft.WindowsAzure.StorageClient;
using OneTransitAPI.Data;
using Microsoft.WindowsAzure;
using Stancer.GTFSEngine;

namespace OneTransitAPI.Transit.Common
{
    public class GTFS : IWebService
    {
        private DatabaseDataContext db;
        private GTFS_Agency agency;

        public GTFS(Agency transitAgency) : base(transitAgency)
        {
            db = new DatabaseDataContext();

            agency = (from a in db.GTFS_Agencies where a.ID == transitAgency.AgencyID select a).Single();
        }

        public override List<Route> GetRoutes()
        {
            List<Route> result = new List<Route>();

            var routes = from r in db.GTFS_Routes where r.PartitionKey == agency.PartitionKey select r;
            foreach (var r in routes)
            {
                Route rt = new Route();
                rt.ID = r.ID;
                rt.ShortName = r.ShortName;
                rt.LongName = r.LongName;
                rt.Type = (int)r.Type;

                result.Add(rt);
            }

            return result;
        }

        public override Stop GetStop(string stopid)
        {
            var stop = (from s in db.GTFS_Stops where s.PartitionKey == agency.PartitionKey &&
                                                      s.ID.ToUpper() == stopid.ToUpper() select s).Single();

            Stop result = new Stop();
            result.ID = stop.ID;
            result.Name = stop.Name;
            result.Code = stop.Code;
            result.Latitude = Convert.ToDouble(stop.Latitude);
            result.Longitude = Convert.ToDouble(stop.Longitude);

            return result;
        }

        public override List<Stop> GetStops()
        {
            List<Stop> result = new List<Stop>();

            var stops = from s in db.GTFS_Stops where s.PartitionKey == agency.PartitionKey select s;
            foreach (var s in stops)
            {
                Stop st = new Stop();
                st.ID = s.ID;
                st.Name = s.Name;
                st.Code = s.Code;
                st.Latitude = Convert.ToDouble(s.Latitude);
                st.Longitude = Convert.ToDouble(s.Longitude);

                result.Add(st);
            }

            return result;
        }

        public override List<Stop> GetStopsByLocation(double latitude, double longitude, double radius)
        {
            List<Stop> result = new List<Stop>();

            var stops = from s in db.GTFS_Stops where s.PartitionKey == agency.PartitionKey select s;
            foreach (var s in stops)
            {
                if (Utilities.Distance(latitude, longitude, Convert.ToDouble(s.Latitude), Convert.ToDouble(s.Longitude)) <= radius)
                {
                    Stop st = new Stop();
                    st.ID = s.ID;
                    st.Name = s.Name;
                    st.Code = s.Code;
                    st.Latitude = Convert.ToDouble(s.Latitude);
                    st.Longitude = Convert.ToDouble(s.Longitude);

                    result.Add(st);
                }
            }

            return result;
        }

        public override List<StopTime> GetStopTimes(string stopid)
        {
            List<StopTime> result = new List<StopTime>();

            var utc = new DateTimeOffset(DateTime.UtcNow, TimeSpan.Zero);
            var now = utc.ToOffset(this.TransitAgency.FriendlyTimeZone.GetUtcOffset(utc));

            var tod0 = now.DateTime;
            var tod1 = now.AddHours(2).DateTime;

            var stopTimes = db.GetStopTimes(tod0, tod1, agency.PartitionKey, stopid);

            foreach (var r in stopTimes)
            {
                StopTime t = new StopTime();
                t.RouteShortName = r.ShortName;
                t.RouteLongName = r.LongName;
                t.ArrivalTime = r.ArrivalTime.ToString("hh:mm tt");
                t.DepartureTime = r.DepartureTime.ToString("hh:mm tt");
                t.Type = "scheduled";

                result.Add(t);
            }

            return result;
        }
    }
}
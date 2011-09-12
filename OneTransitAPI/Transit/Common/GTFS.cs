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
        private Engine gtfsEngine;

        public GTFS(Agency transitAgency) : base(transitAgency)
        {
            StorageCredentialsAccountAndKey storageCredentialsAccountAndKey = new StorageCredentialsAccountAndKey(ConfigurationManager.AppSettings["AzureStorageAccount"], ConfigurationManager.AppSettings["AzureStorageKey"]);
            CloudStorageAccount acct = new CloudStorageAccount(storageCredentialsAccountAndKey, true);
            
            CloudBlobClient blobStorage = acct.CreateCloudBlobClient();
            blobStorage.ParallelOperationThreadCount = 1;

            CloudBlobContainer container = blobStorage.GetContainerReference("gtfs");

            AzureStorageSourceDataCollection data = new AzureStorageSourceDataCollection(container, transitAgency.AgencyID.ToLower());

            gtfsEngine = new Engine(data);
        }

        public override List<Route> GetRoutes()
        {
            List<Route> result = new List<Route>();

            foreach (var r in gtfsEngine.Routes)
            {
                Route rt = new Route();
                rt.ID = r.RouteID;
                rt.ShortName = r.ShortName;
                rt.LongName = r.LongName;

                result.Add(rt);
            }

            return result;
        }

        public override Stop GetStop(string stopid)
        {
            var r = (from s in gtfsEngine.Stops where s.ID.ToUpper() == stopid.ToUpper() select s).Single();

            Stop result = new Stop();
            result.ID = r.ID;
            result.Name = r.Name;
            result.Code = r.Code;
            result.Latitude = Convert.ToDouble(r.Lat);
            result.Longitude = Convert.ToDouble(r.Lon);

            return result;
        }

        public override List<Stop> GetStops()
        {
            List<Stop> result = new List<Stop>();

            foreach (var r in gtfsEngine.Stops)
            {
                Stop s = new Stop();
                s.ID = r.ID;
                s.Name = r.Name;
                s.Code = r.Code;
                s.Latitude = Convert.ToDouble(r.Lat);
                s.Longitude = Convert.ToDouble(r.Lon);

                result.Add(s);
            }

            return result;
        }

        public override List<Stop> GetStopsByLocation(double latitude, double longitude, double radius)
        {
            List<Stop> result = new List<Stop>();

            foreach (var r in gtfsEngine.Stops)
            {
                if (Utilities.Distance(latitude, longitude, Convert.ToDouble(r.Lat), Convert.ToDouble(r.Lon)) <= radius)
                {
                    Stop s = new Stop();
                    s.ID = r.ID;
                    s.Name = r.Name;
                    s.Code = r.Code;
                    s.Latitude = Convert.ToDouble(r.Lat);
                    s.Longitude = Convert.ToDouble(r.Lon);

                    result.Add(s);
                }
            }

            return result;
        }

        public override List<StopTime> GetStopTimes(string stopID)
        {
            List<StopTime> result = new List<StopTime>();

            var utc = new DateTimeOffset(DateTime.UtcNow, TimeSpan.Zero);
            var now = utc.ToOffset(this.TransitAgency.FriendlyTimeZone.GetUtcOffset(utc)).ToLocalTime();

            var tod0 = now.TimeOfDay;
            var tod1 = now.AddHours(2).TimeOfDay;

            var sts =
                from st in gtfsEngine.Stop_Times
                let StopID = st.StopID
                where StopID == stopID
                where st.DepartureTime.ToString() != ""
                let DepartureTime = st.DepartureTime
                let ArrivalTime = st.ArrivalTime
                where DepartureTime >= tod0
                where DepartureTime < tod1
                let TripID = st.TripID
                select new
                {
                    StopID,
                    TripID,
                    ArrivalTime,
                    DepartureTime
                };

            var ts =
                from t in gtfsEngine.Trips
                where t.RouteID.ToString() != ""
                let TripID = t.TripID
                let RouteID = t.RouteID
                select new
                {
                    TripID,
                    RouteID,
                };

            var rs =
                from r in gtfsEngine.Routes
                let RouteID = r.RouteID
                let RouteShortName = r.ShortName
                let RouteLongName = r.LongName
                select new
                {
                    RouteID,
                    RouteShortName,
                    RouteLongName
                };

            var tripLookup = ts.ToDictionary(t => t.TripID);
            var routeLookup = rs.ToDictionary(r => r.RouteID);

            var query = from StopTime in sts.ToArray()
                        let Trip = tripLookup[StopTime.TripID]
                        let Route = routeLookup[Trip.RouteID]
                        orderby StopTime.DepartureTime
                        select new
                        {
                            StopTime,
                            Trip,
                            Route,
                        };

            foreach (var r in query)
            {
                StopTime t = new StopTime();
                t.RouteShortName = r.Route.RouteShortName;
                t.RouteLongName = r.Route.RouteLongName;
                t.ArrivalTime = now.Date.Add(r.StopTime.ArrivalTime);
                t.DepartureTime = now.Date.Add(r.StopTime.DepartureTime);
                t.Type = 0;

                if ((from x in result where x.RouteShortName == t.RouteShortName select x).Count() < 2)
                    result.Add(t);
            }

            return result;
        }
    }
}
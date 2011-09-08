using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using System.Configuration;
using Microsoft.WindowsAzure.StorageClient;
using OneTransitAPI.Data;

namespace OneTransitAPI.Transit.Common
{
    public class GTFS : IWebService
    {
        private EnumerableRowCollection<DataRow> Stops;
        private EnumerableRowCollection<DataRow> StopTimes;
        private EnumerableRowCollection<DataRow> Routes;
        private EnumerableRowCollection<DataRow> Trips;

        public GTFS(Agency transitAgency) : base(transitAgency) { }

        public override List<Stop> GetStopsByLocation(double latitude, double longitude, double radius)
        {
            Stops = ImportGTFS(TransitAgency.AgencyID, "stops.txt").AsEnumerable();

            List<Stop> result = new List<Stop>();

            foreach (var r in Stops)
            {
                if (Utilities.Distance(latitude, longitude, r.Field<double>("stop_lat"), r.Field<double>("stop_lon")) <= radius)
                {
                    Stop s = new Stop();
                    s.ID = r.Field<string>("stop_id");
                    s.Name = r.Field<string>("stop_name");
                    s.Code = r.Field<string>("stop_code");
                    s.Latitude = r.Field<double>("stop_lat");
                    s.Longitude = r.Field<double>("stop_lon");

                    result.Add(s);
                }
            }

            return result;
        }

        public override List<StopTime> GetStopTimes(string stopID)
        {
            Stops = ImportGTFS(TransitAgency.AgencyID, "stops.txt").AsEnumerable();
            StopTimes = ImportGTFS(TransitAgency.AgencyID, "stop_times.txt").AsEnumerable();
            Routes = ImportGTFS(TransitAgency.AgencyID, "routes.txt").AsEnumerable();
            Trips = ImportGTFS(TransitAgency.AgencyID, "trips.txt").AsEnumerable();

            List<StopTime> result = new List<StopTime>();

            var now = DateTime.UtcNow;
            var tod0 = now.AddHours(-TransitAgency.TimeZone).TimeOfDay;
            var tod1 = now.AddHours(-TransitAgency.TimeZone).AddHours(2).TimeOfDay;

            if (Convert.ToBoolean(ConfigurationManager.AppSettings["IsDaylightSavingsTime"]) == true)
            {
                tod0 = tod0.Add(new TimeSpan(1, 0, 0));
                tod1 = tod1.Add(new TimeSpan(1, 0, 0));
            }

            var sts =
                from st in StopTimes
                let StopID = st.Field<string>("stop_id")
                where StopID == stopID
                where st["departure_time"].ToString() != ""
                let DepartureTime = st.Field<DateTime>("departure_time").TimeOfDay
                let ArrivalTime = st.Field<DateTime>("arrival_time").TimeOfDay
                where DepartureTime >= tod0
                where DepartureTime < tod1
                let TripID = st.Field<string>("trip_id")
                select new
                {
                    StopID,
                    TripID,
                    ArrivalTime,
                    DepartureTime
                };

            var ts =
                from t in Trips
                where t["route_id"].ToString() != ""
                let TripID = t.Field<string>("trip_id")
                let RouteID = t.Field<string>("route_id")
                select new
                {
                    TripID,
                    RouteID,
                };

            var rs =
                from r in Routes
                let RouteID = r.Field<string>("route_id")
                let RouteShortName = r.Field<string>("route_short_name")
                let RouteLongName = r.Field<string>("route_long_name")
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
                t.ArrivalTime = r.StopTime.ArrivalTime;
                t.DepartureTime = r.StopTime.DepartureTime;
                t.Type = 0;

                if ((from x in result where x.RouteShortName == t.RouteShortName select x).Count() < 2)
                    result.Add(t);
            }

            return result;
        }

        public DataTable ImportGTFS(string blobPath, string blobName)
        {
            Microsoft.WindowsAzure.StorageCredentialsAccountAndKey storageCredentialsAccountAndKey = new Microsoft.WindowsAzure.StorageCredentialsAccountAndKey(ConfigurationManager.AppSettings["AzureStorageAccount"], ConfigurationManager.AppSettings["AzureStorageKey"]);

            Microsoft.WindowsAzure.CloudStorageAccount acct = new Microsoft.WindowsAzure.CloudStorageAccount(storageCredentialsAccountAndKey, true);
            CloudBlobClient client = acct.CreateCloudBlobClient();

            MemoryStream stream = new MemoryStream();
            CloudBlob blob = client.GetBlobReference("gtfs/" + blobPath + "/" + blobName);

            DataTable dt = new DataTable(blobName);
            switch (blobName)
            {
                case "routes.txt":
                    // route_id,agency_id,route_short_name,route_long_name,route_desc,route_type,route_url,route_color,route_text_color
                    dt.Columns.Add("route_id", typeof(string));
                    dt.Columns.Add("agency_id", typeof(string));
                    dt.Columns.Add("route_short_name", typeof(string));
                    dt.Columns.Add("route_long_name", typeof(string));
                    break;
                case "trips.txt":
                    // route_id,service_id,trip_id,trip_headsign,trip_short_name,direction_id,block_id,shape_id
                    dt.Columns.Add("route_id", typeof(string));
                    dt.Columns.Add("service_id", typeof(string));
                    dt.Columns.Add("trip_id", typeof(string));
                    dt.Columns.Add("trip_headsign", typeof(string));
                    break;
                case "stops.txt":
                    // stop_id,stop_code,stop_name,stop_desc,stop_lat,stop_lon,zone_id,stop_url,location_type,parent_station
                    dt.Columns.Add("stop_id", typeof(string));
                    dt.Columns.Add("stop_code", typeof(string));
                    dt.Columns.Add("stop_name", typeof(string));
                    dt.Columns.Add("stop_desc", typeof(string));
                    dt.Columns.Add("stop_lat", typeof(double));
                    dt.Columns.Add("stop_lon", typeof(double));
                    break;
                case "stop_times.txt":
                    // trip_id,arrival_time,departure_time,stop_id,stop_sequence,stop_headsign,pickup_type,drop_off_type,shape_dist_traveled
                    dt.Columns.Add("trip_id", typeof(string));
                    dt.Columns.Add("arrival_time", typeof(DateTime));
                    dt.Columns.Add("departure_time", typeof(DateTime));
                    dt.Columns.Add("stop_id", typeof(string));
                    break;
            }

            blob.DownloadToStream(stream);
            stream.Position = 0;

            using (StreamReader readBlob = new StreamReader(stream))
            {
                string line = readBlob.ReadLine();
                while ((line = readBlob.ReadLine()) != null)
                {
                    switch (TransitAgency.AgencyID)
                    {
                        case "mta":
                            if (line.Contains("\""))
                            {
                                string tmp = line.Split('"')[0] + line.Split('"')[2];
                                line = tmp;
                            }
                            break;
                        case "citybus":
                            line = line.Replace("\"", "");
                            break;
                        case "cta":
                            line = line.Replace("\"", "");
                            break;
                        default:
                            break;
                    }

                    switch (blobName)
                    {
                        case "routes.txt":
                            dt.Rows.Add(new object[] { line.Split(',')[0], line.Split(',')[1], line.Split(',')[2], line.Split(',')[3] });
                            break;
                        case "trips.txt":
                            dt.Rows.Add(new object[] { line.Split(',')[0], line.Split(',')[1], line.Split(',')[2], line.Split(',')[3] });
                            break;
                        case "stops.txt":
                            if (line.Split(',').Count() < 5) continue;
                            if (line.Split(',')[4].Length == 0 ||
                                line.Split(',')[5].Length == 0) continue;
                            dt.Rows.Add(new object[] { line.Split(',')[0], line.Split(',')[1], line.Split(',')[2], line.Split(',')[3], Convert.ToDouble(line.Split(',')[4]), Convert.ToDouble(line.Split(',')[5]) });
                            break;
                        case "stop_times.txt":
                            if (Convert.ToInt32(line.Split(',')[1].Substring(0, 2).Replace(":", "")) > 23 ||
                                Convert.ToInt32(line.Split(',')[2].Substring(0, 2).Replace(":", "")) > 23) continue;
                            DateTime arrival = DateTime.UtcNow.Date.AddHours(Convert.ToDateTime("01/01/1900 " + line.Split(',')[1]).Hour).AddMinutes(Convert.ToDateTime("01/01/1900 " + line.Split(',')[1]).Minute).AddSeconds(Convert.ToDateTime("01/01/1900 " + line.Split(',')[1]).Second);
                            DateTime departure = DateTime.UtcNow.Date.AddHours(Convert.ToDateTime("01/01/1900 " + line.Split(',')[2]).Hour).AddMinutes(Convert.ToDateTime("01/01/1900 " + line.Split(',')[2]).Minute).AddSeconds(Convert.ToDateTime("01/01/1900 " + line.Split(',')[2]).Second);
                            dt.Rows.Add(new object[] { line.Split(',')[0], arrival, departure, line.Split(',')[3] });
                            break;
                        default:
                            dt.Rows.Add(line.Split(','));
                            break;
                    }
                }
            }

            return dt;
        }
    }
}
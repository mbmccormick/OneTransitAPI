using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Configuration;
using System.IO;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;
using Ionic.Zip;
using BlobStorageEngine.Data;
using OneTransitAPI.Common;
using Stancer.GTFSEngine;
using Stancer.GTFSEngine.Entities;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;

namespace BlobStorageEngine
{
    #region DebugTextWriter

    public class DebugTextWriter : System.IO.TextWriter
    {
        public override void Write(char[] buffer, int index, int count)
        {
            System.Diagnostics.Debug.Write(new String(buffer, index, count));
        }

        public override void Write(string value)
        {
            System.Diagnostics.Debug.Write(value);
        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.Default; }
        }
    }

    #endregion

    public class WorkerRole : RoleEntryPoint
    {
        public override void Run()
        {
            WebClient web = new WebClient();
            web.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.CacheIfAvailable);

            while (true)
            {
                Utilities.LogEvent("BlobStorageEngine", "Waking up...");

                DatabaseDataContext db = new DatabaseDataContext();
                db.CommandTimeout = 0;

                DebugTextWriter trace = new DebugTextWriter();
                db.Log = trace;

                var agencies = from a in db.GTFS_Agencies orderby a.Name select a;
                foreach (GTFS_Agency a in agencies)
                {
                    try
                    {
                        if (a.URL == null ||
                            a.URL.Length == 0)
                        {
                            continue;
                        }

                        StringBuilder builder;
                        StringWriter writer;
                        XmlSerializer serializer;
                        XmlDocument doc;

                        #region Download GTFS data

                        Utilities.LogEvent("BlobStorageEngine", "Downloading new GTFS data for " + a.Name + " (" + a.ID + ")...");

                        byte[] rawData = web.DownloadData(a.URL);
                        MemoryStream download = new MemoryStream(rawData);

                        ZipFile gtfsData = ZipFile.Read(download);

                        Dictionary<string, Stream> streamData = new Dictionary<string, Stream>();

                        foreach (ZipEntry file in gtfsData)
                        {
                            MemoryStream output = new MemoryStream();
                            file.Extract(output);

                            output.Seek(0, SeekOrigin.Begin);

                            streamData.Add(file.FileName.ToLower().Replace(".txt", ""), output);
                        }

                        download.Dispose();

                        #endregion

                        Utilities.LogEvent("BlobStorageEngine", "Initializing GTFS Engine...");
                        Engine gtfsEngine = new Engine(new DictionarySourceDataCollection(streamData));

                        Guid newPartitionKey = Guid.NewGuid();
                        Guid oldPartitionKey = a.PartitionKey;

                        #region Update GTFS_Calendar

                        Utilities.LogEvent("BlobStorageEngine", "Uploading records for GTFS_Calendar.");
                        List<GTFS_Calendar> calendars = new List<GTFS_Calendar>();
                        foreach (Calendar c in gtfsEngine.Calendars)
                        {
                            GTFS_Calendar data = new GTFS_Calendar();

                            data.RowKey = Guid.NewGuid();
                            data.PartitionKey = a.PartitionKey;
                            data.ServiceID = c.ServiceID;
                            data.Monday = c.Monday;
                            data.Tuesday = c.Tuesday;
                            data.Wednesday = c.Wednesday;
                            data.Thursday = c.Thursday;
                            data.Friday = c.Friday;
                            data.Saturday = c.Saturday;
                            data.Sunday = c.Sunday;
                            data.StartDate = c.DateStart;
                            data.EndDate = c.DateEnd;

                            calendars.Add(data);
                        }

                        builder = new StringBuilder();
                        writer = new StringWriter(builder);
                        serializer = new XmlSerializer(typeof(List<GTFS_Calendar>));
                        serializer.Serialize(writer, calendars);

                        doc = new XmlDocument();
                        doc.LoadXml(builder.ToString());

                        db.InsertCalendars(XElement.Load(doc.DocumentElement.CreateNavigator().ReadSubtree()), newPartitionKey, oldPartitionKey);

                        #endregion

                        #region Update GTFS_Routes

                        Utilities.LogEvent("BlobStorageEngine", "Uploading records for GTFS_Routes.");
                        List<GTFS_Route> routes = new List<GTFS_Route>();
                        foreach (Route r in gtfsEngine.Routes)
                        {
                            GTFS_Route data = new GTFS_Route();

                            data.RowKey = Guid.NewGuid();
                            data.PartitionKey = a.PartitionKey;
                            data.ID = r.RouteID;
                            data.LongName = r.LongName;
                            data.ShortName = r.ShortName;
                            data.Type = (int)r.RouteType;

                            routes.Add(data);
                        }

                        builder = new StringBuilder();
                        writer = new StringWriter(builder);
                        serializer = new XmlSerializer(typeof(List<GTFS_Route>));
                        serializer.Serialize(writer, routes);

                        doc = new XmlDocument();
                        doc.LoadXml(builder.ToString());

                        db.InsertRoutes(XElement.Load(doc.DocumentElement.CreateNavigator().ReadSubtree()), newPartitionKey, oldPartitionKey);

                        #endregion

                        #region Update GTFS_Stops

                        Utilities.LogEvent("BlobStorageEngine", "Uploading records for GTFS_Stops.");
                        List<GTFS_Stop> stops = new List<GTFS_Stop>();
                        foreach (Stop s in gtfsEngine.Stops)
                        {
                            GTFS_Stop data = new GTFS_Stop();

                            data.RowKey = Guid.NewGuid();
                            data.PartitionKey = a.PartitionKey;
                            data.ID = s.ID;
                            data.Code = s.Code;
                            data.Name = s.Name;
                            data.Latitude = s.Lat;
                            data.Longitude = s.Lon;

                            stops.Add(data);
                        }

                        builder = new StringBuilder();
                        writer = new StringWriter(builder);
                        serializer = new XmlSerializer(typeof(List<GTFS_Stop>));
                        serializer.Serialize(writer, stops);

                        doc = new XmlDocument();
                        doc.LoadXml(builder.ToString());

                        db.InsertStops(XElement.Load(doc.DocumentElement.CreateNavigator().ReadSubtree()), newPartitionKey, oldPartitionKey);

                        #endregion

                        #region Update GTFS_StopTimes

                        Utilities.LogEvent("BlobStorageEngine", "Uploading records for GTFS_StopTimes.");
                        List<GTFS_StopTime> stopTimes = new List<GTFS_StopTime>();
                        foreach (StopTime t in gtfsEngine.Stop_Times)
                        {
                            GTFS_StopTime data = new GTFS_StopTime();

                            data.RowKey = Guid.NewGuid();
                            data.PartitionKey = newPartitionKey;
                            data.StopID = t.StopID;
                            data.TripID = t.TripID;
                            data.StopSequence = t.StopSequence;
                            data.ArrivalTime = new DateTime(2000, 1, 1, t.ArrivalTime.Hours, t.ArrivalTime.Minutes, t.ArrivalTime.Seconds, t.ArrivalTime.Milliseconds);
                            data.DepartureTime = new DateTime(2000, 1, 1, t.DepartureTime.Hours, t.DepartureTime.Minutes, t.DepartureTime.Seconds, t.DepartureTime.Milliseconds);

                            stopTimes.Add(data);
                        }

                        builder = new StringBuilder();
                        writer = new StringWriter(builder);
                        serializer = new XmlSerializer(typeof(List<GTFS_StopTime>));
                        serializer.Serialize(writer, stopTimes);

                        doc = new XmlDocument();
                        doc.LoadXml(builder.ToString());

                        db.InsertStopTimes(XElement.Load(doc.DocumentElement.CreateNavigator().ReadSubtree()), newPartitionKey, oldPartitionKey);

                        #endregion

                        #region Update GTFS_Trips

                        Utilities.LogEvent("BlobStorageEngine", "Uploading records for GTFS_Trips.");
                        List<GTFS_Trip> trips = new List<GTFS_Trip>();
                        foreach (Trip t in gtfsEngine.Trips)
                        {
                            GTFS_Trip data = new GTFS_Trip();

                            data.RowKey = Guid.NewGuid();
                            data.PartitionKey = a.PartitionKey;
                            data.ID = t.TripID;
                            data.RouteID = t.RouteID;
                            data.ServiceID = t.ServiceID;

                            trips.Add(data);
                        }

                        builder = new StringBuilder();
                        writer = new StringWriter(builder);
                        serializer = new XmlSerializer(typeof(List<GTFS_Trip>));
                        serializer.Serialize(writer, trips);

                        doc = new XmlDocument();
                        doc.LoadXml(builder.ToString());

                        db.InsertTrips(XElement.Load(doc.DocumentElement.CreateNavigator().ReadSubtree()), newPartitionKey, oldPartitionKey);

                        #endregion

                        foreach (Stream s in streamData.Values)
                            s.Dispose();

                        Utilities.LogEvent("BlobStorageEngine", "Complete.");
                    }
                    catch (Exception ex)
                    {
                        Utilities.LogEvent("BlobStorageEngine", "An unhandled exception has occurred. Message: " + ex.Message + "; " +
                                                                                                     "Source: " + ex.Source + "; " +
                                                                                                     "TargetSite: " + ex.TargetSite + "; " +
                                                                                                     "StackTrace: " + ex.StackTrace + ";");

                        System.Threading.Thread.Sleep(5000);
                    }
                }

                Utilities.LogEvent("BlobStorageEngine", "Going to sleep.");

                Thread.Sleep(1000 * 60 * 60 * 24 * 7);
            }
        }

        public override bool OnStart()
        {
            RoleEnvironment.Changing += RoleEnvironment_Changing;

            Utilities.LogEvent("BlobStorageEngine", "Ready.");

            return base.OnStart();
        }

        private void RoleEnvironment_Changing(object sender, RoleEnvironmentChangingEventArgs e)
        {
            if (e.Changes.Any(change => change is RoleEnvironmentConfigurationSettingChange))
                e.Cancel = true;
        }
    }
}

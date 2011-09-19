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

namespace BlobStorageEngine
{
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

    public class WorkerRole : RoleEntryPoint
    {
        public override void Run()
        {
            WebClient web = new WebClient();
            web.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.CacheIfAvailable);

            while (true)
            {
                Utilities.LogEvent("BlobStorageEngine", "Waking up...");
                
                try
                {
                    DatabaseDataContext db = new DatabaseDataContext();
                    db.CommandTimeout = 0; // this is VERY bad

                    // DEBUG
                    DebugTextWriter trace = new DebugTextWriter();
                    db.Log = trace;

                    var agencies = from a in db.GTFS_Agencies where a.ID == "mta" select a; // from a in db.GTFS_Agencies orderby a.Name select a;

                    foreach (GTFS_Agency a in agencies)
                    {
                        if (a.URL == null ||
                            a.URL.Length == 0)
                        {
                            continue;
                        }

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

                        Utilities.LogEvent("BlobStorageEngine", "Removing old records from database.");
                        db.ExecuteCommand("DELETE FROM dbo.GTFS_Calendar WHERE PartitionKey = '" + a.PartitionKey + "'");
                        db.ExecuteCommand("DELETE FROM dbo.GTFS_Routes WHERE PartitionKey = '" + a.PartitionKey + "'");
                        db.ExecuteCommand("DELETE FROM dbo.GTFS_Stops WHERE PartitionKey = '" + a.PartitionKey + "'");
                        db.ExecuteCommand("DELETE FROM dbo.GTFS_StopTimes WHERE PartitionKey = '" + a.PartitionKey + "'");
                        db.ExecuteCommand("DELETE FROM dbo.GTFS_Trips WHERE PartitionKey = '" + a.PartitionKey + "'");

                        Utilities.LogEvent("BlobStorageEngine", "Submitting Changes...");
                        db.SubmitChanges();

                        Utilities.LogEvent("BlobStorageEngine", "Initializing GTFS Engine...");
                        Engine gtfsEngine = new Engine(new DictionarySourceDataCollection(streamData));

                        Utilities.LogEvent("BlobStorageEngine", "Inserting new records to database...");

                        Utilities.LogEvent("BlobStorageEngine", "Queuing records for GTFS_Calendar.");
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

                            db.GTFS_Calendars.InsertOnSubmit(data);
                        }

                        Utilities.LogEvent("BlobStorageEngine", "Queuing records for GTFS_Routes.");
                        foreach (Route r in gtfsEngine.Routes)
                        {
                            GTFS_Route data = new GTFS_Route();

                            data.RowKey = Guid.NewGuid();
                            data.PartitionKey = a.PartitionKey;
                            data.ID = r.RouteID;
                            data.LongName = r.LongName;
                            data.ShortName = r.ShortName;
                            data.Type = (int)r.RouteType;

                            db.GTFS_Routes.InsertOnSubmit(data);
                        }

                        Utilities.LogEvent("BlobStorageEngine", "Uploading to GTFS_Stops.");
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

                            db.GTFS_Stops.InsertOnSubmit(data);
                        }

                        Utilities.LogEvent("BlobStorageEngine", "Queuing records for GTFS_StopTimes.");
                        foreach (StopTime t in gtfsEngine.Stop_Times)
                        {
                            GTFS_StopTime data = new GTFS_StopTime();

                            data.RowKey = Guid.NewGuid();
                            data.PartitionKey = a.PartitionKey;
                            data.StopID = t.StopID;
                            data.TripID = t.TripID;
                            data.StopSequence = t.StopSequence;
                            data.ArrivalTime = t.ArrivalTime;
                            data.DepartureTime = t.DepartureTime;

                            db.GTFS_StopTimes.InsertOnSubmit(data);
                        }

                        Utilities.LogEvent("BlobStorageEngine", "Queuing records for GTFS_Trips.");
                        foreach (Trip t in gtfsEngine.Trips)
                        {
                            GTFS_Trip data = new GTFS_Trip();

                            data.RowKey = Guid.NewGuid();
                            data.PartitionKey = a.PartitionKey;
                            data.ID = t.TripID;
                            data.RouteID = t.RouteID;
                            data.ServiceID = t.ServiceID;

                            db.GTFS_Trips.InsertOnSubmit(data);
                        }

                        Utilities.LogEvent("BlobStorageEngine", "Submitting changes...");
                        db.SubmitChanges();
                        
                        foreach (Stream s in streamData.Values)
                            s.Dispose();

                        Utilities.LogEvent("BlobStorageEngine", "Complete.");
                    }
                }
                catch (Exception ex)
                {
                    Utilities.LogEvent("BlobStorageEngine", "An unhandled exception has occurred. Message: " + ex.Message + "; " +
                                                                                                "Source: " + ex.Source + "; " +
                                                                                                "TargetSite: " + ex.TargetSite + "; " +
                                                                                                "StackTrace: " + ex.StackTrace + ";");

                    System.Threading.Thread.Sleep(5000);
                }

                Utilities.LogEvent("BlobStorageEngine", "Going to sleep.");

                Thread.Sleep(1000 * 60 * 60 * 72);
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

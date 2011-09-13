using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;
using OneTransitAPI;
using OneTransitAPI.Common;
using OneTransitAPI.Data;
using System.IO;
using Ionic.Zip;
using System.Configuration;

namespace BlobStorageEngine
{
    public class WorkerRole : RoleEntryPoint
    {
        private CloudBlobContainer container;

        public override void Run()
        {
            Utilities.LogEvent("BlobStorageEngine", "Initializing.");

            WebClient web = new WebClient();
            web.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.CacheIfAvailable);

            while (true)
            {
                Utilities.LogEvent("BlobStorageEngine", "Waking up...");

                try
                {
                    DatabaseDataContext db = new DatabaseDataContext();
                    var agencies = from a in db.Agencies orderby a.Name select a;

                    foreach (Agency a in agencies)
                    {
                        if (a.GTFSUrl == null ||
                            a.GTFSUrl.Length == 0)
                        {
                            continue;
                        }

                        Utilities.LogEvent("BlobStorageEngine", "Downloading new GTFS data for " + a.Name + " (" + a.AgencyID + ")...");

                        byte[] rawData = web.DownloadData(a.GTFSUrl);
                        MemoryStream download = new MemoryStream(rawData);

                        ZipFile gtfsData = ZipFile.Read(download);

                        foreach (ZipEntry file in gtfsData)
                        {
                            Utilities.LogEvent("BlobStorageEngine", "Saving " + file.FileName + " to storage.");

                            CloudBlob blob = container.GetBlobReference(a.AgencyID.ToLower() + "/" + file.FileName.ToLower());

                            MemoryStream output = new MemoryStream();
                            file.Extract(output);

                            output.Seek(0, SeekOrigin.Begin);

                            blob.UploadFromStream(output);
                        }

                        Utilities.LogEvent("BlobStorageEngine", "Complete.");
                    }
                }
                catch (StorageClientException ex)
                {
                    Utilities.LogEvent("BlobStorageEngine", "An unhandled exception has occurred. Message: " + ex.Message + "; " +
                                                                                                "Source: " + ex.Source + "; " +
                                                                                                "TargetSite: " + ex.TargetSite + "; " +
                                                                                                "StackTrace: " + ex.StackTrace + ";");

                    System.Threading.Thread.Sleep(5000);
                }

                Utilities.LogEvent("BlobStorageEngine", "Going to sleep.");
                Thread.Sleep(1000 * 60 * 60 * 24 * 7); // execute every 7 days
            }
        }

        public override bool OnStart()
        {
            DiagnosticMonitor.Start("Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString");

            RoleEnvironment.Changing += RoleEnvironmentChanging;

            CloudStorageAccount.SetConfigurationSettingPublisher((configName, configSetter) =>
            {
                try
                {
                    configSetter(RoleEnvironment.GetConfigurationSettingValue(configName));
                }
                catch (RoleEnvironmentException ex)
                {
                    Utilities.LogEvent("BlobStorageEngine", "An unhandled exception has occurred. Message: " + ex.Message + "; " +
                                                                                                 "Source: " + ex.Source + "; " +
                                                                                                 "TargetSite: " + ex.TargetSite + "; " +
                                                                                                 "StackTrace: " + ex.StackTrace + ";");
                    System.Threading.Thread.Sleep(5000);
                }
            });

            var storageAccount = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");

            CloudBlobClient blobStorage = storageAccount.CreateCloudBlobClient();
            blobStorage.ParallelOperationThreadCount = 1;

            container = blobStorage.GetContainerReference("gtfs");

            Utilities.LogEvent("BlobStorageEngine", "Creating storage container...");

            bool storageInitialized = false;
            while (!storageInitialized)
            {
                try
                {
                    container.CreateIfNotExist();

                    var permissions = container.GetPermissions();
                    permissions.PublicAccess = BlobContainerPublicAccessType.Container;
                    container.SetPermissions(permissions);

                    storageInitialized = true;
                }
                catch (StorageClientException e)
                {
                    if (e.ErrorCode == StorageErrorCode.TransportError)
                    {
                        Utilities.LogEvent("BlobStorageEngine", "Storage services initialization failure. Check your storage account configuration settings.");
                        System.Threading.Thread.Sleep(5000);
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return base.OnStart();
        }

        private void RoleEnvironmentChanging(object sender, RoleEnvironmentChangingEventArgs e)
        {
            if (e.Changes.Any(change => change is RoleEnvironmentConfigurationSettingChange))
                e.Cancel = true;
        }
    }
}

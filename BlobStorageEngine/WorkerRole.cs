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
            Trace.WriteLine("Initializing.", "Information");

            WebClient web = new WebClient();
            web.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.CacheIfAvailable);

            while (true)
            {
                Trace.WriteLine("Waking up...", "Information");

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

                        Trace.WriteLine("Downloading new GTFS data for " + a.Name + " (" + a.AgencyID + ")...", "Information");

                        byte[] rawData = web.DownloadData(a.GTFSUrl);
                        MemoryStream download = new MemoryStream(rawData);

                        ZipFile gtfsData = ZipFile.Read(download);

                        foreach (ZipEntry file in gtfsData)
                        {
                            Trace.WriteLine("Saving " + file.FileName + " to storage.", "Information");

                            CloudBlob blob = container.GetBlobReference(a.AgencyID.ToLower() + "/" + file.FileName.ToLower());

                            MemoryStream output = new MemoryStream();
                            file.Extract(output);

                            output.Seek(0, SeekOrigin.Begin);

                            blob.UploadFromStream(output);
                        }

                        Trace.WriteLine("Complete.", "Information");
                    }
                }
                catch (StorageClientException e)
                {
                    Trace.TraceError("Exception when processing queue item. Message: '{0}'", e.Message);
                    System.Threading.Thread.Sleep(5000);
                }

                Trace.WriteLine("Going to sleep.", "Information");
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
                catch (RoleEnvironmentException e)
                {

                    Trace.TraceError(e.Message);
                    System.Threading.Thread.Sleep(5000);
                }
            });

            var storageAccount = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");

            CloudBlobClient blobStorage = storageAccount.CreateCloudBlobClient();
            blobStorage.ParallelOperationThreadCount = 1;

            container = blobStorage.GetContainerReference("gtfs");

            Trace.TraceInformation("Creating storage container...");

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
                        Trace.TraceError("Storage services initialization failure. Check your storage account configuration settings. If running locally, ensure that the Development Storage service is running. Message: '{0}'", e.Message);
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

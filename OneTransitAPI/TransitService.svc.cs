using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.ServiceModel.Activation;
using System.Configuration;
using OneTransitAPI.Data;
using OneTransitAPI.Transit;
using OneTransitAPI.Transit.Common;

namespace OneTransitAPI
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public partial class TransitService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, UriTemplate = "agencies/getList?key={key}")]
        public List<Agency> GetAgencies(string key)
        {
            this.ValidateKey(key);
            
            DatabaseDataContext db = new DatabaseDataContext();

            List<Agency> result = new List<Agency>();

            foreach (var r in db.GTFS_Agencies.OrderBy(a => a.Name))
            {
                Agency a = new Agency();
                a.AgencyID = r.ID;
                a.Name = r.Name;
                a.State = r.State;
                a.TimeZone = r.TimeZone;

                result.Add(a);
            }

            return result;
        }

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, UriTemplate = "routes/getList?key={key}&agency={agencyid}")]
        public List<Route> GetRoutes(string key, string agencyid)
        {
            this.ValidateKey(key);

            IWebService webService = SelectWebService(agencyid);
            return webService.GetRoutes().OrderBy(r => r.ID).ToList<Route>();
        }

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, UriTemplate = "stops/getInfo?key={key}&agency={agencyid}&stop={stopid}")]
        public Stop GetStop(string key, string agencyid, string stopid)
        {
            this.ValidateKey(key);

            IWebService webService = SelectWebService(agencyid);
            return webService.GetStop(stopid);
        }

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, UriTemplate = "stops/getList?key={key}&agency={agencyid}")]
        public List<Stop> GetStops(string key, string agencyid)
        {
            this.ValidateKey(key);

            IWebService webService = SelectWebService(agencyid);
            return webService.GetStops().OrderBy(s => s.ID).ToList<Stop>();
        }

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, UriTemplate = "stops/getListByLocation?key={key}&agency={agencyid}&lat={latitude}&lon={longitude}&radius={radius}")]
        public List<Stop> GetStopsByLocation(string key, string agencyid, double latitude, double longitude, double radius)
        {
            this.ValidateKey(key);

            IWebService webService = SelectWebService(agencyid);
            return webService.GetStopsByLocation(latitude, longitude, radius).OrderBy(s => Utilities.Distance(s.Latitude, s.Longitude, latitude, longitude)).ToList<Stop>();
        }

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, UriTemplate = "stops/getTimes?key={key}&agency={agencyid}&stop={stopid}")]
        public List<StopTime> GetStopTimes(string key, string agencyid, string stopid)
        {
            this.ValidateKey(key);

            IWebService webService = SelectWebService(agencyid);
            return webService.GetStopTimes(stopid).OrderBy(t => t.ArrivalTime).ToList<StopTime>();
        }

        private void ValidateKey(string key)
        {
            DatabaseDataContext db = new DatabaseDataContext();
            
            if ((from c in db.Consumers where c.ConsumerKey.ToString() == key select c).Count() == 0)
            {
                throw new UnauthorizedAccessException("The provided Consumer Key was not found.");
            }
        }

        private IWebService SelectWebService(string agencyid)
        {
            DatabaseDataContext db = new DatabaseDataContext();
            var agency = new Agency((from a in db.GTFS_Agencies where a.ID == agencyid select a).Single());

            IWebService webService;
            switch (agency.AgencyID)
            {
                case "bart":
                    webService = new BART(agency);
                    break;
                case "citybus":
                    webService = new CityBus(agency);
                    break;
                case "cta":
                    webService = new CTA(agency);
                    break;
                case "mta":
                    webService = new MTA(agency);
                    break;
                case "seattle":
                    webService = new Seattle(agency);
                    break;
                case "wmata":
                    webService = new WMATA(agency);
                    break;
                default:
                    webService = new GTFS(agency);
                    break;
            }

            webService.TransitAgency = agency;

            return webService;
        }
    }
}

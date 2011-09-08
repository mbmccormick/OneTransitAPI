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
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, UriTemplate = "agencies/getList")]
        public List<Agency> GetTransitAgencies()
        {
            DatabaseDataContext db = new DatabaseDataContext();
            return db.Agencies.ToList<Agency>();
        }

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, UriTemplate = "stops/getListByLocation?agency={agencyid}&lat={latitude}&lon={longitude}&radius={radius}")]
        public List<Stop> GetStopsByLocation(string agencyid, double latitude, double longitude, double radius)
        {
            IWebService webService = SelectWebService(agencyid);
            return webService.GetStopsByLocation(latitude, longitude, radius);
        }

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, UriTemplate = "stops/getTimes?agency={agencyid}&stop={stopid}")]
        public List<StopTime> GetStopTimes(string agencyid, string stopid)
        {
            IWebService webService = SelectWebService(agencyid);
            return webService.GetStopTimes(stopid);
        }

        private IWebService SelectWebService(string agencyid)
        {
            DatabaseDataContext db = new DatabaseDataContext();
            var agency = (from a in db.Agencies where a.AgencyID == agencyid select a).Single();
                        
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
                case "lanta":
                    webService = new GTFS(agency);
                    break;
                case "manchester":
                    webService = new GTFS(agency);
                    break;
                case "massport":
                    webService = new GTFS(agency);
                    break;
                case "msl":
                    webService = new GTFS(agency);
                    break;
                case "mta":
                    webService = new GTFS(agency);
                    break;
                case "riderta":
                    webService = new GTFS(agency);
                    break;
                case "sdmts":
                    webService = new GTFS(agency);
                    break;
                case "seattle":
                    webService = new Seattle(agency);
                    break;
                case "stanford":
                    webService = new GTFS(agency);
                    break;
                case "translink":
                    webService = new GTFS(agency);
                    break;
                case "ttc":
                    webService = new GTFS(agency);
                    break;
                case "wmata":
                    webService = new WMATA(agency);
                    break;
                default:
                    webService = new RouteShout(agency);
                    break;
            }

            webService.TransitAgency = agency;

            return webService;
        }
    }
}

﻿using System;
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
        public List<Agency> GetAgencies()
        {
            DatabaseDataContext db = new DatabaseDataContext();

            List<Agency> result = new List<Agency>();

            foreach (var r in db.TransitAgencies)
            {
                Agency a = new Agency();
                a.AgencyID = r.AgencyID;
                a.Name = r.Name;
                a.State = r.State;
                a.TimeZone = r.TimeZone;

                result.Add(a);
            }

            return result;
        }

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, UriTemplate = "routes/getList?agency={agencyid}")]
        public List<Route> GetRoutes(string agencyid)
        {
            IWebService webService = SelectWebService(agencyid);
            return webService.GetRoutes().OrderBy(r => r.ID).ToList<Route>();
        }

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, UriTemplate = "stops/getInfo?agency={agencyid}&stop={stopid}")]
        public Stop GetStop(string agencyid, string stopid)
        {
            IWebService webService = SelectWebService(agencyid);
            return webService.GetStop(stopid);
        }

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, UriTemplate = "stops/getList?agency={agencyid}")]
        public List<Stop> GetStops(string agencyid)
        {
            IWebService webService = SelectWebService(agencyid);
            return webService.GetStops().OrderBy(s => s.ID).ToList<Stop>();
        }

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, UriTemplate = "stops/getListByLocation?agency={agencyid}&lat={latitude}&lon={longitude}&radius={radius}")]
        public List<Stop> GetStopsByLocation(string agencyid, double latitude, double longitude, double radius)
        {
            IWebService webService = SelectWebService(agencyid);
            return webService.GetStopsByLocation(latitude, longitude, radius).OrderBy(s => Utilities.Distance(s.Latitude, s.Longitude, latitude, longitude)).ToList<Stop>();
        }

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, UriTemplate = "stops/getTimes?agency={agencyid}&stop={stopid}")]
        public List<StopTime> GetStopTimes(string agencyid, string stopid)
        {
            IWebService webService = SelectWebService(agencyid);
            return webService.GetStopTimes(stopid).OrderBy(t => t.ArrivalTime).ToList<StopTime>();
        }

        private IWebService SelectWebService(string agencyid)
        {
            DatabaseDataContext db = new DatabaseDataContext();
            var agency = new Agency((from a in db.TransitAgencies where a.AgencyID == agencyid select a).Single());

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

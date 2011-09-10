using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using OneTransitAPI.Data;

namespace OneTransitAPI.Transit.Common
{
    public abstract class IWebService
    {
        public Agency TransitAgency;
        public string APIKey;
        
        protected IWebService(Agency transitAgency)
        {
            this.TransitAgency = transitAgency;
        }

        public abstract List<Route> GetRoutes();

        public abstract Stop GetStop(string stopid);

        public abstract List<Stop> GetStops();
        
        public abstract List<Stop> GetStopsByLocation(double latitude, double longitude, double radius);

        public abstract List<StopTime> GetStopTimes(string stopid);
    }
}
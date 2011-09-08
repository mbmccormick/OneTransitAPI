using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneTransitAPI.Data
{
    public class StopTime
    {
        public string StopID;
        public string RouteShortName;
        public string RouteLongName;
        public TimeSpan ArrivalTime;
        public TimeSpan DepartureTime;
        public int Type;

        public StopTime() { }
    }
}
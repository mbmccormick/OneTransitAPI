﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneTransitAPI.Data
{
    public class StopTime
    {
        public string RouteShortName;
        public string RouteLongName;
        public string ArrivalTime;
        public string DepartureTime;
        public string Type;

        public StopTime() { }
    }
}
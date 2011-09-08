using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneTransitAPI.Data
{
    public class Stop
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Stop() { }
    }
}
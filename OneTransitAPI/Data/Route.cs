using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneTransitAPI.Data
{
    public class Route
    {
        public string ID { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public int Type { get; set; }

        public Route() { }
    }
}
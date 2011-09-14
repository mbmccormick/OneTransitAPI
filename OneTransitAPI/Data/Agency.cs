using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OneTransitAPI.Common;

namespace OneTransitAPI.Data
{
    public class Agency
    {
        public string AgencyID { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string TimeZone { get; set; }

        public Agency() { }

        public Agency(TransitAgency a)
        {
            this.AgencyID = a.AgencyID;
            this.Name = a.Name;
            this.State = a.State;
            this.TimeZone = a.TimeZone;
        }

        public TimeZoneInfo FriendlyTimeZone
        {
            get
            {
                return Utilities.OlsonTimeZoneToTimeZoneInfo(this.TimeZone);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using OneTransitAPI.Data;
using OneTransitAPI.Transit.Common;

namespace OneTransitAPI.Transit
{
    public class BART : IWebService
    {
        public BART(Agency transitAgency) : base(transitAgency)
        {
            this.APIKey = ConfigurationManager.AppSettings["BARTKey"];
        }
        
        public override List<Stop> GetStopsByLocation(double latitude, double longitude, double radius)
        {
            GTFS engine = new GTFS(this.TransitAgency);
            List<Stop> result = engine.GetStopsByLocation(latitude, longitude, radius);

            return result;
        }

        public override List<StopTime> GetStopTimes(string stopID)
        {
            System.Net.WebClient client = new System.Net.WebClient();

            DataSet ds = new DataSet();
            ds.ReadXml(client.OpenRead("http://api.bart.gov/api/etd.aspx?cmd=etd&key=" + APIKey + "&orig=" + stopID));

            List<StopTime> result = new List<StopTime>();

            foreach (DataRow r in ds.Tables["etd"].Rows)
            {
                foreach (DataRow s in ds.Tables["estimate"].Rows)
                {
                    if (s["etd_Id"].ToString() == r["etd_Id"].ToString())
                    {
                        StopTime t = new StopTime();
                        t.RouteShortName = r["abbreviation"].ToString();
                        t.RouteLongName = r["destination"].ToString();

                        if (s["minutes"].ToString() == "Arrived")
                            t.ArrivalTime = DateTime.UtcNow.AddHours(-this.TransitAgency.TimeZone).TimeOfDay;
                        else
                            t.ArrivalTime = DateTime.UtcNow.AddHours(-this.TransitAgency.TimeZone).AddMinutes(Convert.ToInt32(s["minutes"].ToString().Trim())).TimeOfDay;
                        t.DepartureTime = t.ArrivalTime;
                        t.Type = 1;

                        if (Convert.ToBoolean(ConfigurationManager.AppSettings["IsDaylightSavingsTime"]) == true)
                        {
                            t.ArrivalTime = t.ArrivalTime.Add(new TimeSpan(1, 0, 0));
                            t.DepartureTime = t.DepartureTime.Add(new TimeSpan(1, 0, 0));
                        }

                        if ((from x in result where x.RouteShortName == t.RouteShortName select x).Count() < 2)
                            result.Add(t);
                    }
                }
            }

            return result;
        }
    }
}
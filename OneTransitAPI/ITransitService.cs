using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using OneTransitAPI.Data;
using OneTransitAPI.Transit;
using OneTransitAPI.Transit.Common;

namespace OneTransitAPI
{
    [ServiceContract]
    public interface ITransitService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        List<Agency> GetTransitAgencies();

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        List<Stop> GetStopsByLocation(string agencyid, double latitude, double longitude, double radius);

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        List<StopTime> GetStopTimes(string agencyid, string stopid);
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Net;

namespace AKVR.Services.TrafficLocation
{
    public class TrafficLocationMapper : BaseMapper
    {
        public TrafficLocationMapper(WebClient webClient, LocalClient localClient, SessionClient sessionClient) : base(webClient, localClient, sessionClient)
        {
        }

        public List<TrafficLocationModel> SelectAll()
        {
            List<TrafficLocationModel> trafficLocations;
            
            try
            {
                string json = this.getJSON("metadata/stations", true, 3600);
                trafficLocations = JsonConvert.DeserializeObject<List<TrafficLocationModel>>(json);
            }
            catch (Exception ex)
            {
                trafficLocations = new List<TrafficLocationModel>();
                Debug.WriteLine("AKVR:SelectAll FAILED: " + ex.Message);
            }
            
            return trafficLocations;
        }

    }
}
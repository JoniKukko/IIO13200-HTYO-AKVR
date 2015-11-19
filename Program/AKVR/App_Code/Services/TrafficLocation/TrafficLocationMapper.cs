using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;


namespace AKVR.Services.TrafficLocation
{
    public class TrafficLocationMapper : BaseMapper
    {
        
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
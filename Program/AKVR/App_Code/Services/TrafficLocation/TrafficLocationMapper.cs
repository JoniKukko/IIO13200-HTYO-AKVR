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

        public TrafficLocationModel SelectByStationName(string stationName)
        {
            TrafficLocationModel trafficLocation = null;
            List<TrafficLocationModel> list;

            try
            {
                string json = this.getJSON("metadata/stations", true, 3600);

                list = JsonConvert.DeserializeObject<List<TrafficLocationModel>>(json);

                trafficLocation = list.Find(m => m.stationName == stationName);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("AKVR:SelectByStationName FAILED: " + ex.Message);
            }

            if (trafficLocation == null)
            {
                trafficLocation = new TrafficLocationModel();
            }


            return trafficLocation;
        }

        public List<TrafficLocationModel> SelectListByStationName(string stationName)
        {
            List<TrafficLocationModel> trafficLocations = null;

            try
            {
                string json = this.getJSON("metadata/stations", true, 3600);

                trafficLocations = JsonConvert.DeserializeObject<List<TrafficLocationModel>>(json);

                trafficLocations = trafficLocations.FindAll(m => m.stationName.StartsWith(stationName));

            }
            catch (Exception ex)
            {
                Debug.WriteLine("AKVR:SelectListByStationName FAILED: " + ex.Message);
            }

            if (trafficLocations == null)
            {
                trafficLocations = new List<TrafficLocationModel>();
            }


            return trafficLocations;
        }

    }
}
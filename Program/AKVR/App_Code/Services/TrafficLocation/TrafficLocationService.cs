using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;


namespace AKVR.Services.TrafficLocation
{
    public class TrafficLocationService
    {
        // from here...
        private TrafficLocationMapper Mapper { get; set; }

        public TrafficLocationService(TrafficLocationMapper mapper)
        {
            this.Mapper  = mapper;
        }
        // ...to here should be in every service class



        public TrafficLocationModel SelectByStationName(string stationName)
        {
            TrafficLocationModel trafficLocation = null;
            List<TrafficLocationModel> list = this.Mapper.SelectAll();
            trafficLocation = list.Find(m => m.stationName == stationName);

            if (trafficLocation == null)
            {
                Debug.WriteLine("TrafficLocation not found with full stationName - trying with \"StartsWith\"");
                trafficLocation = list.Find(m => m.stationName.StartsWith(stationName));
            } else
            {
                Debug.WriteLine("TrafficLocation found with full stationName");
            }

            return (trafficLocation == null)
                ? new TrafficLocationModel()
                : trafficLocation;
        }



        public List<TrafficLocationModel> SelectListByStationName(string stationName)
        {
            List<TrafficLocationModel> trafficLocations = this.Mapper.SelectAll();
            return trafficLocations.FindAll(m => m.stationName.StartsWith(stationName));
        }



        public List<TrafficLocationModel> SelectAll()
        {
            return this.Mapper.SelectAll();
        }

    }
}
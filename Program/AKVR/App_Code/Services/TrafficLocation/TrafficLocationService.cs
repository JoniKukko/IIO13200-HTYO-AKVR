using System;
using System.Collections.Generic;
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
            return this.Mapper.SelectByStationName(stationName);
        }


        public List<TrafficLocationModel> SelectListByStationName(string stationName)
        {
            return this.Mapper.SelectListByStationName(stationName);
        }

    }
}
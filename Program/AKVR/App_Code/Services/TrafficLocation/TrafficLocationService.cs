using System.Collections.Generic;
using System.Diagnostics;


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



        // hakee mapperilta kaiken ja parsii itse yhden mallin
        // kokonaisen nimen tai nimen alun perusteella
        public TrafficLocationModel SelectByStationName(string stationName)
        {
            // palautusarvo
            TrafficLocationModel trafficLocation = null;

            // haetaan kaikki ja etsitään kokonaisella nimellä
            List<TrafficLocationModel> list = this.Mapper.SelectAll();
            trafficLocation = list.Find(m => m.stationName == stationName);

            // jos ei löydy niin haetaan nimen alulla
            if (trafficLocation == null)
            {
                Debug.WriteLine("TrafficLocation not found with full stationName - trying with \"StartsWith\"");
                trafficLocation = list.Find(m => m.stationName.StartsWith(stationName));
            } else
            {
                Debug.WriteLine("TrafficLocation found with full stationName");
            }

            // jos nimen alullakaan ei löydy niin palautetaan ainakin
            // tyhjä malli
            return (trafficLocation == null)
                ? new TrafficLocationModel()
                : trafficLocation;
        }



        // palauttaa listan malleista nimen alun perusteella
        public List<TrafficLocationModel> SelectListByStationName(string stationName)
        {
            // haetaan kaikki
            List<TrafficLocationModel> trafficLocations = this.Mapper.SelectAll();
            // palautetaan vain ne joiden nimi alkaa annetulla parametrillä
            return trafficLocations.FindAll(m => m.stationName.StartsWith(stationName));
        }



        // Nothing special to do, passing call to mapper
        public List<TrafficLocationModel> SelectAll()
        {
            return this.Mapper.SelectAll();
        }

    }
}
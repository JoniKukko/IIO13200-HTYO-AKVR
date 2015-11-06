namespace AKVR.Services.TrafficLocation
{
    class Model
    {
        // boolean   Onko liikennepaikalla kaupallista matkustajaliikennettä
        public bool passengerTraffic { get; set; }

        // string   Liikennepaikan maatunnus
        public string countryCode { get; set; }

        // string     Liikennepaikan nimi
        public string stationName { get; set; }

        // string   Liikennepaikan lyhenne
        public string stationShortCode { get; set; }

        // 1-9999   Liikennepaikan maakohtainen UIC-koodi
        public int stationUICCode { get; set; }

        // decimal   Liikennepaikan latitude "WGS 84"-muodossa
        public decimal latitude { get; set; }

        // decimal   Liikennepaikan longitudi "WGS 84"-muodossa
        public decimal longitude { get; set; }

        // string   Liikennepaikan tyyppi. STATION = asema, 
        // STOPPING_POINT = seisake, TURNOUT_IN_THE_OPEN_LINE = linjavaihde
        public string type { get; set; }
    }
}

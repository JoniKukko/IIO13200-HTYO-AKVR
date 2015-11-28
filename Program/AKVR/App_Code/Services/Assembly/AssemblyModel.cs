using System;
using System.Collections.Generic;

namespace AKVR.Services.Assembly
{

    public class AssemblyModel
    {
        // 1-99999   Junan numero. Esim junan "IC 59" junanumero on 59
        public int trainNumber { get; set; }

        //  date   Junan ensimmäisen lähdön päivämäärä
        public DateTime departureDate { get; set; }

        // 1-9999   Junan operoiman operaattorin UIC-koodi
        public int operatorUICCode { get; set; }

        //  vr, vr-track, destia, ...   Lista operaattoreista löytyy täältä.
        public string operatorShortCode { get; set; }

        //  lähiliikenne, kaukoliikenne, tavaraliikenne
        public string trainCategory { get; set; }

        //  P, S, IC, IC2, MUS, etc.
        public string trainType { get; set; }

        //  positive integer  Versionumero, jossa juna on viimeksi muuttunut
        public long version { get; set; }

        // Kuvaa junan yhtä matkaosuutta, joka ajetaan samalla kokoonpanolla 
        public JourneySection
            
            journeySections { get; set; }

    }



    public struct JourneySection
    {
        // Aloitus
        public TimeTableRow beginTimeTableRow { get; set; }

        // Lopetus
        public TimeTableRow endTimeTableRow { get; set; }

        // Kokoonpanon veturit
        public List<Locomotive> locomotives { get; set; }

        // Wagon|none   Kokoonpanon vaunut
        public List<Wagon> wagons { get; set; }

        //  positive integer   junankokonaispituus metreissä
        public int totalLength { get; set; }

        // positive integer   Junan kokoonpanolle ilmoitettu maksiminopeus 
        // kilometreina tunnissa
        public int maximumSpeed { get; set; }

    }



    public struct TimeTableRow
    {
        // string   Aseman lyhennekoodi
        public string stationShortCode { get; set; }

        // 1-9999   Aseman UIC-koodi
        public int stationcUICCode { get; set; }

        // "FI" or "RU"
        public string countryCode { get; set; }

        //  "Arrival" or "Departure"  Pysähdyksen tyyppi
        public string type { get; set; }

        // datetime    Aikataulun mukainen pysähtymis- tai lähtöaika
        public DateTime scheduledTime { get; set; }

    }



    public struct Locomotive
    {
        // positive integer   Veturin paikka kokoonpanossa. Pienin numero on junan kärjessä
        public int location { get; set; }

        // SR1, SR2, ...   Veturin tyyppi
        public string locomotiveType { get; set; }

        // Diesel, Sähkö, ...   Veturin vetovoimalaji
        public string powerType { get; set; }

    }



    public struct Wagon
    {
        // integer   Vaunun paikka kokoonpanossa. Pienin numero on junan kärjessä
        public int location { get; set; }

        // 0-99   Vaunun myyntinumero. Lukee esimerkiksi matkustajan junalipussa. 0 jos ei tiedossa.
        public int salesNumber { get; set; }

        // positive integer|none   Vaunun pituus senttimetreinä
        public int length { get; set; }

        //  true|none   Onko vaunussa leikkipaikka
        public bool playground { get; set; }

        // true|none   Onko vaunussa lemmikkivaunu
        public bool pet { get; set; }

        //  true|none   Onko vaunussa ravintolavaunu
        public bool catering { get; set; }

        // true|none   Onko vaunussa videonäyttömahdollisuus
        public bool video { get; set; }

        // true|none   Onko vaunussa matkatavarasäilytysmahdollisuus
        public bool luggage { get; set; }

        // true|none   Saako vaunussa tupakoida
        public bool smoking { get; set; }

        // true|none   Onko vaunussa invalidiystävällinen
        public bool disabled { get; set; }

        // string|none  Suomalainen sarjatunnus vaunulle. Ilmaisee vaunun tyypin sekä 
        // vaunun palvelut. Kaikille vaunuille ei välttämättä löydy sarjatunnusta. 
        // Lisätietoa http://fi.wikipedia.org/wiki/Sarjatunnus
        public string wagonType { get; set; }

    }

}

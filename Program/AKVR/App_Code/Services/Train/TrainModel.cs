using System;

namespace AKVR.Services.Train
{
    

    public class TrainModel
    {
        // 1-99999  Junan numero. Esim junan "IC 59" junanumero on 59
        public int trainNumber { get; set; }

        // date  Junan ensimmäisen lähdön päivämäärä
        public DateTime departureDate { get; set; }

        // 1-9999   Junan operoiman operaattorin UIC-koodi
        public int operatorUICCode { get; set; }

        //  vr, vr-track, destia, ...  Lista operaattoreista löytyy täältä.
        public string operatorShortCode { get; set; }

        //  IC, P, S, ...
        public string trainType { get; set; }

        // lähiliikenne, kaukoliikenne, tavaraliikenne, ...
        public string trainCategory { get; set; }

        // string|none   Z, K, N....
        public string commuterLineID { get; set; }

        // true/false  Onko juna tällä hetkellä kulussa
        public bool runningCurrently { get; set; }

        // true/false    Totta, jos junan peruminen on tehty 10 vuorokauden sisällä.
        // Yli 10 vuorokautta sitten peruttuja junia ei palauteta rajapinnassa laisinkaan.
        public bool cancelled { get; set; }

        //  positive integer   Versionumero, jossa juna on viimeksi muuttunut
        public long version { get; set; }

        // Kuvaa saapumisia ja lähtöjä liikennepaikoilta. 
        // Järjestetty reitin mukaiseen järjestykseen.
        public Timetable[] timeTableRows { get; set; }

        // kertoo sen mitä tossa lukee
        public int AverageDelay { get; set; }
        public int MaxDelay { get; set; }

        public string FullTrainName
        {
            get
            {
                return trainType + trainNumber.ToString();
            }
        }
    }


    public struct Timetable
    {
        // Tähän ei ollut selitetty - oletetaan boolean
        public bool trainStopping { get; set; }

        //  string  Aseman lyhennekoodi
        public string stationShortCode { get; set; }

        // 1-9999  Aseman UIC-koodi
        public int stationcUICCode { get; set; }

        // "FI" tai "RU"
        public string countryCode { get; set; }

        // "Arrival" or "Departure"  Pysähdyksen tyyppi
        public string type { get; set; }

        // boolean|none  Onko pysähdys kaupallinen. Annettu vain pysähdyksille, 
        // ei läpiajoille. Mikäli junalla on osaväliperumisia, saattaa viimeinen perumista edeltävä pysähdys jäädä virheellisesti ei-kaupalliseksi.
        public bool commercialStop { get; set; }

        // string|none  Suunniteltu raidenumero, jolla juna pysähtyy tai 
        // jolta se lähtee. Operatiivisissa häiriötilanteissa raide voi olla muu.
        public string commercialTrack { get; set; }

        // true/false   Totta, jos lähtö tai saapuminen on peruttu
        public bool cancelled { get; set; }

        // datetime   Aikataulun mukainen pysähtymis- tai lähtöaika
        public DateTime scheduledTime { get; set; }

        // datetime|none  Ennuste. Tyhjä jos juna ei ole matkalla
        public DateTime liveEstimateTime { get; set; }

        // datetime|none  Aika jolloin juna saapui tai lähti asemalta
        public DateTime actualTime { get; set; }

        //  integer  Vertaa aikataulun mukaista aikaa ennusteeseen tai 
        // toteutuneeseen aikaan ja kertoo erotuksen minuutteina  
        public int differenceInMinutes { get; set; }

        // Syytiedot. Kuvaavat syitä miksi juna oli myöhässä tai 
        // etuajassa pysähdyksellä. Kaikkia syyluokkia ja -tietoja ei julkaista.
        public CategoryCode[] causes { get; set; }

    }


    public struct CategoryCode
    {
        // Yleisen syyluokan tunnus. Lista syyluokista löytyy 
        // osoitteesta metadata/cause_category_code
        public string categoryCode { get; set; }

        // string|none   Tarkempi syykoodin tunnus. Lista syykoodeista löytyy 
        // osoitteesta metadata/detailed-cause-category-codes
        public string detailedCategoryCode { get; set; }

    }
}

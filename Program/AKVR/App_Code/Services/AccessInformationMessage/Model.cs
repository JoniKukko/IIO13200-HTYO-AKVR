using System;

namespace AKVR.Services.AccessInformationMessage
{
    public class Model
    {
        //  positive integer  Kulkutietoviestin yksilöivä numero.
        public int id { get; set; }

        // positive integer  Versionumero, jossa kulkutietoviesti on viimeksi muuttunut
        public int version { get; set; }

        // 1-99999   Junan numero. Esim junan "IC 59" junanumero on 59
        public int trainNumber { get; set; }

        // date   Junan ensimmäisen lähdön päivämäärä
        public DateTime departureDate { get; set; }

        // date   Tapahtuman ajanhetki
        public DateTime timestamp { get; set; }

        // string   Tapahtuman raideosuuden tunniste. 
        // Lista raideosuuksista löytyy täältä.
        public string trackSection { get; set; }

        // string|none   Seuraava raideosuuden tunniste, jolle juna ajaa.
        public string nextTrackSection { get; set; }

        // string|none   Raideosuuden tunniste, jolta juna tuli.
        public string previousTrackSection { get; set; }

        // string   Liikennepaikan tunniste, jonka alueella raideosuus on. 
        // Lista liikennepaikoista löytyy täältä.
        public string station { get; set; }

        // string|none   Liikennepaikan tunniste, jonka alueella juna aiemmin oli.
        public string nextStation { get; set; }

        // string|none   Liikennepaikan tunniste, jonka alueelle juna ajaa seuraavaksi.
        public string previousStation { get; set; }

        // string  Tapahtuman tyyppi. 
        // OCCUPY tarkoittaa, että juna varasi raideosuuden. 
        // RELEASE tarkoittaa, että juna vapautti raideosuuden.
        public string type { get; set; }

    }
}

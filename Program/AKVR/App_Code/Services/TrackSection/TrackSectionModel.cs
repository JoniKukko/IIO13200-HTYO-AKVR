using System.Collections.Generic;

namespace AKVR.Services.TrackSection
{

    public class TrackSectionModel
    {
        // Raideosuuden liikennepaikan lyhenne. 
        // Lista liikennepaikoista löytyy täältä.
        public string station { get; set; }

        //  Raideosuuden tunnus. Yksilöivä tieto.
        public string trackSectionCode { get; set; }

        // Raideosuuden sijainnit. Raideosuudella voi olla monta 
        // sijaintia, jos se sijaitsee usealla eri ratanumerolla.
        public List<Range> ranges { get; set; }
    }



    public struct Range
    {
        // positive integer  Sijainnin yksilöivä numero
        public int id { get; set; }

        // Sijainnin alkukohta
        public Location startLocation { get; set; }

        // Sijainnin loppukohta
        public Location endLocation { get; set; }
    }



    public struct Location
    {
        // Ratanumero (string oletettu)
        public string track { get; set; }

        // Sijainnin kilometri-komponentti. 
        // Sijainti kilometreina rataverkon nollapisteestä.
        public int kilometres { get; set; }

        // Sijainnin metri-komponentti. Eli ylijäävä osuus kilometreistä.
        public int metres { get; set; }
    }

}

namespace AKVR.Services.Operator
{

    public class Model
    {
        // positive integer  Operaattorin yksilöivä tunnus
        public int id { get; set; }

        // string     Operaattorin nimi
        public string operatorName { get; set; }

        // string   Operaattorin lyhenne
        public string operatorShortCode { get; set; }

        // 1-9999   Operaattorin UIC-koodi
        public int operatorUICCode { get; set; }

        // TrainNumber|none   Operaattorin käytössäolevat junanumeroavaruudet.
        public TrainNumber[] trainNumbers { get; set; }

    }



    public struct TrainNumber
    {
        // positive integer   Junanumeroavaruuden yksilöivä tunnus
        public int id { get; set; }

        // 1-99999  Junanumeroiden alaraja
        public int bottomLimit { get; set; }

        // 1-99999  Junanumeroiden yläraja
        public int topLimit { get; set; }

        // string  Junalaji
        public string trainCategory { get; set; }
    }

}

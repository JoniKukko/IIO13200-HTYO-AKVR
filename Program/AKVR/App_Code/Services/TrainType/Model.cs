namespace AKVR.Services.TrainType
{

    public class Model
    {
        // string   Junatyypin nimi
        public string name { get; set; }

        // TrainCategory   Junalaji
        public TrainCategory trainCategory { get; set; }
    }



    public struct TrainCategory
    {
        // string   Junalajin nimi
        public string name { get; set; }
    }

}

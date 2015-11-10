using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AKVR.Services.Train
{


    public class TrainMapper : BaseMapper
    {


        // Palauttaa mallin junanumeron perusteella
        public TrainModel SelectByTrainNumber(int trainNumber)
        {
            // palautusarvo
            TrainModel train;
            try
            {
                Debug.WriteLine("AKVR:TrainMapper:SelectByTrainNumber(" + trainNumber.ToString() + ")");
                // Haetaan json
                string json = this.getJSON("live-trains/" + trainNumber.ToString(), false);
                // yritetään deserialisoida
                train = JsonConvert.DeserializeObject<TrainModel>(json);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("AKVR:TrainMapper:SelectByTrainNumber(" + trainNumber.ToString() + ") - FAILED: " + ex.Message);
                // palautetaan ainakin tyhjä sitten..
                train = new TrainModel();
            }
            return train;
        }



        // palauttaa malli listan aseman lyhenteellä
        public List<TrainModel> SelectByStationShortCode(string stationShortCode)
        {
            List<TrainModel> trains;
            try
            {
                Debug.WriteLine("AKVR:TrainMapper:SelectByStationShortCode(" + stationShortCode + ")");
                // Haetaan json
                string json = this.getJSON("live-trains?station=" + stationShortCode, true);
                // yritetään deserialisoida
                trains = JsonConvert.DeserializeObject<List<TrainModel>>(json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("AKVR:TrainMapper:SelectByStationShortCode(" + stationShortCode + ") FAILED: " + ex.Message);
                // palautetaan ainakin tyhjä sitten..
                trains = new List<TrainModel>();
            }
            return trains;
        }


    }


}
